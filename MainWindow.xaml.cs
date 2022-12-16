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

            //Creating a connection to dB, doing nothing really,
            //just to remove initial delay when conecting first time.
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
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (UserDbContext context = new())
            {
                UserRepository u = new(context);

                //LoginUser gets the userobject in return if match is found in dB
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
                    MessageBox.Show("Wrong username or password...");
                }
                pbPassword.Clear();
            }
        }

        //Open register window
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
