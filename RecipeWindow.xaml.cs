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
        public RecipeWindow(int loginId)
        {
            InitializeComponent();
            LoadRecipeListview("");
        }

        private void LoadRecipeListview(string s)
        {
            if (s == "")
            {
                using (RecipeDbContext context = new())
                {
                    UnitOfWork uow = new(context);
                    List<Recipe> recipes = uow.recipeRepo.GetAllRecipes();
                    AddRecipesToListView(recipes);
                }
            }
            else
            {
                //sökfunktion efter s
            }
        }

        private void AddRecipesToListView(List<Recipe> recipes)
        {
            foreach (Recipe recipe in recipes)
            {
                ListViewItem nItem = new();
                nItem.Content = recipe.Name;
                nItem.Tag = recipe;
            }
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
