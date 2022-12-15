using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private bool canEdit = false;
        private int cRecipeId;
        public DetailsWindow(int cRecipeId, bool canEdit)
        {
            InitializeComponent();
            this.cRecipeId = cRecipeId;
            using (RecipeDbContext context = new())
            {
                UnitOfWork uow = new(context);
                Recipe cRecipe = uow.RecipeRepo.GetRecipeById(cRecipeId);
                FillInformation(cRecipe);
            }
            this.canEdit = canEdit;
        }

        private void FillInformation(Recipe cRecipe)
        {
            tbRecipeName.Text = cRecipe.Name;
            foreach (Tag tag in cRecipe.Tags)
            {
                ListViewItem nItem = new();
                nItem.Content = $"#{tag.Name}";
                nItem.Tag = tag;
                lvTags.Items.Add(nItem);
            }
            foreach (Ingredient ingredient in cRecipe.Ingredients)
            {
                ListViewItem nItem = new();
                nItem.Content = $"({ingredient.Quantity}) {ingredient.Name} ";
                nItem.Tag = ingredient;
                lvIngredients.Items.Add(nItem);
            }
            foreach (Step step in cRecipe.Steps)
            {
                ListViewItem nItem = new();
                nItem.Content = $"{step.Order}. {step.Description}";
                nItem.Tag = step;
                lvSteps.Items.Add(nItem);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbRecipeName.Text.Trim().Length < 1)
            {
                lblRecipeName.Foreground = new SolidColorBrush(Colors.Red);
                MessageBox.Show("You need to name your recipe.");
                return;
            }
            using (RecipeDbContext context = new())
            {
                UnitOfWork uow = new(context);
                Recipe dbRecipe = uow.RecipeRepo.GetRecipeById(cRecipeId);
                if (dbRecipe != null)
                {
                    dbRecipe.Tags.Clear();
                    foreach (Tag tag in GetTagList())
                    {
                        Tag? dbTag = uow.TagRepo.GetTagByName(tag.Name);
                        if (dbTag != null)
                        {
                            dbRecipe.Tags.Add(dbTag);
                        }
                        else
                        {
                            dbRecipe.Tags.Add(tag);
                        }
                    }
                    dbRecipe.Name = tbRecipeName.Text;
                    dbRecipe.Steps = GetStepList();
                    dbRecipe.Ingredients = GetIngredientsList();
                    uow.RecipeRepo.UpdateRecipe(dbRecipe);
                    uow.SaveChanges();
                }
            }
            ((RecipeWindow)this.Owner).LoadRecipeListview("");
            this.Owner.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (canEdit)
            {
                btnSave.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Hidden;
                tbRecipeName.IsEnabled = true;
                tbTagName.IsEnabled = true;
                btnAddTag.IsEnabled = true;
                tbIngredientName.IsEnabled = true;
                tbIngredientQuantity.IsEnabled = true;
                btnAddIngredient.IsEnabled = true;
                tbStepName.IsEnabled = true;
                btnAddStep.IsEnabled = true;
                lvTags.IsEnabled = true;
                btnRemoveTag.IsEnabled = true;
                lvIngredients.IsEnabled = true;
                btnRemoveIngredient.IsEnabled = true;
                lvSteps.IsEnabled = true;
                btnRemoveStep.IsEnabled = true;
            }
            else
                MessageBox.Show("You can only edit your own recipes.");
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            lblTags.Foreground = new SolidColorBrush(Colors.Black);
            if (tbTagName.Text.Trim().Length < 1)
            {
                lblTags.Foreground = new SolidColorBrush(Colors.Red); return;
            }
            Tag nTag = new()
            {
                Name = tbTagName.Text,
            };
            ListViewItem nItem = new();
            nItem.Content = nTag.Name;
            nItem.Tag = nTag;
            lvTags.Items.Add(nItem);
            tbTagName.Clear();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            lblIngredient.Foreground = new SolidColorBrush(Colors.Black);
            lblQuantity.Foreground = new SolidColorBrush(Colors.Black);
            if (tbIngredientName.Text.Trim().Length < 1)
            {
                lblIngredient.Foreground = new SolidColorBrush(Colors.Red); return;
            }
            if (tbIngredientQuantity.Text.Trim().Length < 1)
            {
                lblQuantity.Foreground = new SolidColorBrush(Colors.Red); return;
            }
            Ingredient nIngredient = new()
            {
                Name = tbIngredientName.Text,
                Quantity = tbIngredientQuantity.Text
            };
            ListViewItem nItem = new();
            nItem.Content = $"{nIngredient.Quantity} - {nIngredient.Name}";
            nItem.Tag = nIngredient;
            lvIngredients.Items.Add(nItem);
            tbIngredientName.Clear();
            tbIngredientQuantity.Clear();
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            lblStep.Foreground = new SolidColorBrush(Colors.Black);
            if (tbStepName.Text.Trim().Length < 1)
            {
                lblStep.Foreground = new SolidColorBrush(Colors.Red); return;
            }
            Step nStep = new()
            {
                Description = tbStepName.Text
            };
            ListViewItem nItem = new();
            nItem.Content = $"{tbStepName.Text}";
            nItem.Tag = nStep;
            lvSteps.Items.Add(nItem);
            tbStepName.Clear();
        }

        private List<Ingredient> GetIngredientsList()
        {
            List<Ingredient> list = new();
            foreach (ListViewItem item in lvIngredients.Items)
            {
                Ingredient ingredient = item.Tag as Ingredient;
                list.Add(ingredient);
            }
            return list;
        }
        private List<Step> GetStepList()
        {
            int order = 1;
            List<Step> list = new();
            foreach (ListViewItem item in lvSteps.Items)
            {
                Step step = item.Tag as Step;
                step.Order = order;
                order++;
                list.Add(step);
            }
            return list;
        }
        private List<Tag> GetTagList()
        {
            List<Tag> list = new();
            foreach (ListViewItem item in lvTags.Items)
            {
                Tag tag = item.Tag as Tag;
                list.Add(tag);
            }
            return list;
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            lvTags.Items.Remove(lvTags.SelectedItem);
        }

        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            lvIngredients.Items.Remove(lvIngredients.SelectedItem);
        }

        private void btnRemoveStep_Click(object sender, RoutedEventArgs e)
        {
            lvSteps.Items.Remove(lvSteps.SelectedItem);
        }
    }
}
