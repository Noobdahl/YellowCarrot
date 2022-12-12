using System.Windows;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
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
        }
    }
}
