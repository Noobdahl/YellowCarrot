using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            //Track current recipe id
            this.cRecipeId = cRecipeId;
            //Establishing connection to recipe dB
            using (RecipeDbContext context = new())
            {
                UnitOfWork uow = new(context);
                //Get current recipe and sends it to method
                Recipe? cRecipe = uow.RecipeRepo.GetRecipeById(cRecipeId);
                FillInformation(cRecipe);
            }
            //Track if current user are allowed to edit
            this.canEdit = canEdit;
        }

        //Seeds all fields with information from recieved recipe
        private void FillInformation(Recipe cRecipe)
        {
            //Tries to load pic from picUrl in recipe
            try
            {
                var uri = new Uri(cRecipe.picUrl);
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;
            }
            //If it fails, load standard icon
            catch
            {
                image.Source = new BitmapImage(new Uri($@"/Images/yclogo.png", UriKind.Relative));
            }
            //Filling textboxes and listviews
            tbURL.Text = cRecipe.picUrl;
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
            //If no recipename is entered, save is not accepted
            if (tbRecipeName.Text.Trim().Length < 1)
            {
                lblRecipeName.Foreground = new SolidColorBrush(Colors.Red);
                MessageBox.Show("You need to name your recipe.");
                return;
            }
            //Checks if ingredients, steps or tags are missing
            string missing = "";
            if (lvIngredients.Items.Count < 1)
                missing += "- Ingredients\n";
            if (lvSteps.Items.Count < 1)
                missing += "- Steps\n";
            if (lvTags.Items.Count < 1)
                missing += "- Tags\n";
            //If any is missing, ask if user wants to continue and present all missing items
            if (lvIngredients.Items.Count < 1 || lvSteps.Items.Count < 1 || lvTags.Items.Count < 1)
            {
                if (MessageBox.Show($"This recipe is missing:\n\n{missing}\n\nDo you still want to continue?", "Delete recipe", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                //No
                {
                    //If no, stop the method
                    return;
                }
                else
                //Yes
                {
                    //If yes, just continue
                }
            }
            using (RecipeDbContext context = new())
            {
                UnitOfWork uow = new(context);
                //Gets recipe from dB
                Recipe? dbRecipe = uow.RecipeRepo.GetRecipeById(cRecipeId);
                if (dbRecipe != null)
                {
                    //Clear all tags on the recipe
                    dbRecipe.Tags.Clear();
                    //Get all tags from listview and loop through them
                    foreach (Tag tag in GetTagList())
                    {
                        //Try to fetch matching tag in dB
                        Tag? dbTag = uow.TagRepo.GetTagByName(tag.Name);
                        //If found, add the already existing dB-version of the tag
                        if (dbTag != null)
                        {
                            dbRecipe.Tags.Add(dbTag);
                        }
                        else
                        //Else, add the newly created tag to the recipe
                        {
                            dbRecipe.Tags.Add(tag);
                        }
                    }
                    //Replace the rest of properties from textboxes and listviews
                    dbRecipe.picUrl = tbURL.Text.Trim();
                    dbRecipe.Name = tbRecipeName.Text;
                    dbRecipe.Steps = GetStepList();
                    dbRecipe.Ingredients = GetIngredientsList();
                    uow.RecipeRepo.UpdateRecipe(dbRecipe);
                    uow.SaveChanges();
                }
            }
            //Run the method in previous window that reloads the recipe listview
            ((RecipeWindow)this.Owner).LoadRecipeListview("");
            //Open recipewindow 
            this.Owner.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Changes will not be saved, are you sure you want to continue?", "Delete recipe", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //No
            {
                //If no, stop the method
                return;
            }
            else
            //Yes
            {
                //If yes, just continue
            }
            this.Owner.Show();
            this.Close();
        }
        //When edit is pressed, enable and show UI
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
                tbURL.IsEnabled = true;
                btnLoadImage.IsEnabled = true;
            }
            else
                MessageBox.Show("You can only edit your own recipes.");
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            //Sets text color in label to black
            lblTags.Foreground = new SolidColorBrush(Colors.Black);
            //If user attempts to add an empty tag, change textcolor and break method
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
            //Sets text color in labels to black
            lblIngredient.Foreground = new SolidColorBrush(Colors.Black);
            lblQuantity.Foreground = new SolidColorBrush(Colors.Black);
            //If user attempts to add an empty ingredientname or quantity, change textcolor and break method
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
            //Sets text color in label to black
            lblStep.Foreground = new SolidColorBrush(Colors.Black);
            //If user attempts to add an empty tag, change textcolor and break method
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

        //Convert ingredients listviewitems to ingredients and returns as a list
        private List<Ingredient> GetIngredientsList()
        {
            List<Ingredient> list = new();
            foreach (ListViewItem item in lvIngredients.Items)
            {
                Ingredient? ingredient = item.Tag as Ingredient;
                list.Add(ingredient);
            }
            return list;
        }
        //Convert steps listviewitems to steps and returns as a list
        private List<Step> GetStepList()
        {
            int order = 1;
            List<Step> list = new();
            foreach (ListViewItem item in lvSteps.Items)
            {
                Step? step = item.Tag as Step;
                step.Order = order;
                order++;
                list.Add(step);
            }
            return list;
        }
        //Convert tags listviewitems to tags and returns as a list
        private List<Tag> GetTagList()
        {
            List<Tag> list = new();
            foreach (ListViewItem item in lvTags.Items)
            {
                Tag? tag = item.Tag as Tag;
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
        //Attempt to load image from users input url
        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri(tbURL.Text);
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;
            }
            catch
            {
                MessageBox.Show("Couldn't find image in this url.");
            }
        }
    }
}
