using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
        private bool isAdmin = false;
        public RecipeWindow(int loginId, bool isAdmin)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
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
            using (UserDbContext context = new())
            {
                UserRepository userRepo = new(context);
                using (RecipeDbContext _context = new())
                {
                    UnitOfWork uow = new(_context);
                    foreach (Recipe recipe in recipes)
                    {
                        ListViewItem nItem = new();
                        nItem.Content = $"{recipe.Name} - by {userRepo.GetUserNameFromId(recipe.UserId)}\n{uow.TagRepo.GetAllTagsFromRecipeById(recipe.RecipeId)}";
                        nItem.Tag = recipe;
                        lvRecipes.Items.Add(nItem);
                    }
                }
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
                try
                {

                    ListViewItem? sItem = lvRecipes.SelectedItem as ListViewItem;
                    Recipe? sRecipe = sItem.Tag as Recipe;
                    if (sRecipe.UserId == loginId || isAdmin)
                    {
                        using (RecipeDbContext context = new())
                        {
                            UnitOfWork uow = new(context);
                            uow.RecipeRepo.DeleteRecipe(sRecipe);
                            uow.SaveChanges();
                        }
                        LoadRecipeListview("");
                    }
                    else
                        MessageBox.Show("This recipe does not belong to you, cannot delete!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oops, something went wrong!\n" + ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadRecipeListview(tbSearch.Text);
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            Recipe sRecipe = GetSelectedRecipe();
            DetailsWindow detailsWindow = new(sRecipe.RecipeId, sRecipe.UserId == loginId || isAdmin);
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
            PreviewCurrentRecipe();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
        private void PreviewCurrentRecipe()
        {
            if (lvRecipes.SelectedItem == null)
            {
                return;
            }
            ListViewItem? item = lvRecipes.SelectedItem as ListViewItem;
            Recipe? cRecipe = item.Tag as Recipe;
            if (cRecipe != null)
            {
                //Loading name
                lblcRName.Content = cRecipe.Name;
                //Loading picture
                try
                {
                    var uri = new Uri(cRecipe.picUrl);
                    var bitmap = new BitmapImage(uri);
                    cRecipeimage.Source = bitmap;
                }
                catch
                {
                    cRecipeimage.Source = new BitmapImage(new Uri($@"/Images/yclogo.png", UriKind.Relative));
                }
                //Loading tags
                tbcRTags.Clear();
                tbcRTags.Text = "Tags:\n";
                foreach (Tag tag in cRecipe.Tags)
                {
                    tbcRTags.Text += $"#{tag.Name} ";
                }
                //Loading Ingredients
                tbcRIngredients.Clear();
                tbcRIngredients.Text = "Ingredients:\n";
                foreach (Ingredient ingredient in cRecipe.Ingredients)
                {
                    tbcRIngredients.Text += $"{ingredient.Quantity} - {ingredient.Name}\n";
                }
                //Loading Steps
                tbcRSteps.Clear();
                tbcRSteps.Text = "Steps:\n";
                foreach (Step step in cRecipe.Steps)
                {
                    tbcRSteps.Text += $"{step.Order}. {step.Description}\n";
                }
            }
        }
    }
}
