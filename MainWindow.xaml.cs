using System.Windows;
using YellowCarrot.Data;
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
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (UserDbContext context = new())
            {
                UserRepository u = new(context);

                //LoginUser gets UserId in return
                int loginId = u.LoginUser(tbUsername.Text, pbPassword.Password);
                if (loginId > 0)
                {
                    RecipeWindow recipeWindow = new(loginId);
                    recipeWindow.Owner = this;
                    recipeWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrecto!");
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Owner = this;
            regWindow.Show();
            this.Hide();
        }
    }
}
