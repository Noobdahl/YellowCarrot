using System;
using System.Collections.Generic;
using System.Reflection;
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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        private int loginId;
        public AddRecipeWindow(int loginId)
        {
            InitializeComponent();
            //Tracks currently logged in users id
            this.loginId = loginId;
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
                Recipe nRecipe = new()
                {
                    Name = tbRecipeName.Text,
                    UserId = loginId,
                    Steps = GetStepList(),
                    Ingredients = GetIngredientsList(),
                    picUrl = tbURL.Text.Trim()
                };
                //Get all tags from listview and loop through them
                foreach (Tag tag in GetTagList())
                {
                    //Try to fetch matching tag in dB
                    Tag? dbTag = uow.TagRepo.GetTagByName(tag.Name);
                    //If found, add the already existing dB-version of the tag
                    if (dbTag != null)
                    {
                        nRecipe.Tags.Add(dbTag);
                    }
                    //Else, add the newly created tag to the recipe
                    else
                    {
                        nRecipe.Tags.Add(tag);
                    }
                }
                //Add new recipe to dB
                uow.RecipeRepo.CreateNewRecipe(nRecipe);
                uow.SaveChanges();
            }
            //Run the method in previous window that reloads the recipe listview
            ((RecipeWindow)this.Owner).LoadRecipeListview("");
            //Open recipewindow 
            this.Owner.Show();
            this.Close();
        }

        //Convert ingredients listviewitems to ingredients and returns as a list
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

        //Convert steps listviewitems to steps and returns as a list
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

        //Convert tags listviewitems to tags and returns as a list
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Recipe is not created, are you sure you want to continue?", "Delete recipe", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
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
                Name = tbIngredientName.Text.Trim(),
                Quantity = tbIngredientQuantity.Text.Trim(),
            };
            ListViewItem nItem = new();
            nItem.Content = $"{nIngredient.Quantity} - {nIngredient.Name}";
            nItem.Tag = nIngredient;
            lvIngredients.Items.Add(nItem);
            tbIngredientName.Clear();
            tbIngredientQuantity.Clear();
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
                Name = tbTagName.Text.Trim(),
            };
            ListViewItem nItem = new();
            nItem.Content = $"#{nTag.Name}";
            nItem.Tag = nTag;
            lvTags.Items.Add(nItem);
            tbTagName.Clear();
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
                Description = tbStepName.Text.Trim()
            };
            ListViewItem nItem = new();
            nItem.Content = $"{nStep.Description}";
            nItem.Tag = nStep;
            lvSteps.Items.Add(nItem);
            tbStepName.Clear();
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
