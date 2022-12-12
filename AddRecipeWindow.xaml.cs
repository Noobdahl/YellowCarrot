using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        private int loginId;
        private int order = 1;
        public AddRecipeWindow(int loginId)
        {
            InitializeComponent();
            this.loginId = loginId;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Recipe nRecipe = new()
            {
                Name = tbRecipeName.Text,
                UserId = loginId,
                Tags = GetTagList(),
                Steps = GetStepList(),
                Ingredients = GetIngredientsList()
            };
            using (RecipeDbContext context = new())
            {
                UnitOfWork uow = new(context);
                uow.RecipeRepo.CreateNewRecipe(nRecipe);
                uow.SaveChanges();
            }
            ((RecipeWindow)this.Owner).LoadRecipeListview("");
            this.Owner.Show();
            this.Close();
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
            List<Step> list = new();
            foreach (ListViewItem item in lvSteps.Items)
            {
                Step step = item.Tag as Step;
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient nIngredient = new()
            {
                Name = tbIngredientName.Text,
                Quantity = tbIngredientQuantity.Text,
            };
            ListViewItem nItem = new();
            nItem.Content = $"{nIngredient.Name} x{nIngredient.Quantity}";
            nItem.Tag = nIngredient;
            lvIngredients.Items.Add(nItem);
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            Tag nTag = new()
            {
                Name = tbTagName.Text,
            };
            ListViewItem nItem = new();
            nItem.Content = nTag.Name;
            nItem.Tag = nTag;
            lvTags.Items.Add(nItem);
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            Step nStep = new()
            {
                Order = order,
                Description = tbStepName.Text
            };
            ListViewItem nItem = new();
            nItem.Content = $"{order}. {tbStepName.Text}";
            nItem.Tag = nStep;
            lvSteps.Items.Add(nItem);
            order++;
        }
    }
}
