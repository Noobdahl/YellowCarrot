using System.Windows;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //TODO matcha passwords
            User nUser = new()
            {
                Name = tbUsername.Text,
                Password = pbPassword.Password,
                IsAdmin = false
            };
            using (UserDbContext context = new())
            {
                UserRepository u = new(context);
                if (u.AddUser(nUser))
                {
                    this.Owner.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username is already taken!");
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
