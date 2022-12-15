using System.Windows;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Removing intial delay for connection to DB.
            using (RecipeDbContext context = new())
                {
                    UnitOfWork uow = new(context);
                uow.RecipeRepo.GetSearchResult("");
                }
            using (UserDbContext context = new())
            {
                UserRepository u = new(context);
                u.GetUserNameFromId(1);
            }

            tbUsername.Text = "Micke";
            pbPassword.Password = "asd";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (UserDbContext context = new())
            {
                UserRepository u = new(context);

                //LoginUser gets the userobject in return
                User? loggedInUser = u.LoginUser(tbUsername.Text, pbPassword.Password);

                if (loggedInUser != null)
                {
                    RecipeWindow recipeWindow = new(loggedInUser.UserId, loggedInUser.IsAdmin);
                    recipeWindow.Owner = this;
                    recipeWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrecto!");
                }
                pbPassword.Clear();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Owner = this;
            regWindow.Show();
            pbPassword.Clear();
            this.Hide();
        }
    }
}
