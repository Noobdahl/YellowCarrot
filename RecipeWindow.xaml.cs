using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        int loginId;
        public RecipeWindow(int loginId)
        {
            InitializeComponent();
            this.loginId = loginId;
            LoadRecipeListview("");
        }

        public void LoadRecipeListview(string s)
        {
            lvRecipes.Items.Clear();
            ToggleButtons(false);
            using (RecipeDbContext context = new())
            {
                UnitOfWork uow = new(context);
                if (s == "")
                {
                    AddRecipesToListView(uow.RecipeRepo.GetAllRecipes());
                }
                else
                {
                    AddRecipesToListView(uow.RecipeRepo.GetSearchResult(s));
                }
            }
        }
        private void ToggleButtons(bool toggle)
        {
            btnDelete.IsEnabled = toggle;
            btnDetails.IsEnabled = toggle;
        }

        private void AddRecipesToListView(List<Recipe> recipes)
        {
            foreach (Recipe recipe in recipes)
            {
                ListViewItem nItem = new();
                nItem.Content = recipe.Name;
                nItem.Tag = recipe;
                lvRecipes.Items.Add(nItem);
            }
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new(loginId);
            addRecipeWindow.Owner = this;
            addRecipeWindow.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this recipe?", "Delete recipe", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //No
            {

            }
            else
            //Yes
            {
                ToggleButtons(false);
                ListViewItem sItem = lvRecipes.SelectedItem as ListViewItem;
                Recipe sRecipe = sItem.Tag as Recipe;
                if (sRecipe.UserId == loginId)
                {
                    using (RecipeDbContext context = new())
                    {
                        UnitOfWork uow = new(context);
                        uow.RecipeRepo.DeleteRecipe(sRecipe);
                        uow.SaveChanges();
                    }
                    LoadRecipeListview("");
                    MessageBox.Show("Successfully deleted!");

                }
                else
                    MessageBox.Show("This recipe does not belong to you, cannot delete!");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadRecipeListview(tbSearch.Text);
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            Recipe sRecipe = GetSelectedRecipe();
            DetailsWindow detailsWindow = new(sRecipe);
            detailsWindow.Owner = this;
            detailsWindow.Show();
            this.Hide();
        }
        private Recipe GetSelectedRecipe()
        {
            ListViewItem sItem = lvRecipes.SelectedItem as ListViewItem;
            return sItem.Tag as Recipe;
        }

        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleButtons(true);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
