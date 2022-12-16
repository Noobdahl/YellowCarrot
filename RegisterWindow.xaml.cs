using System;
using System.Linq;
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
            try
            {
                if (tbUsername.Text.Count() < 4)
                    throw new Exception("Username must be atleast 4 characters long.");
                else if (pbPassword.Password.Count() < 4)
                    throw new Exception("Password must be atleast 4 characters long.");
                else if (pbPassword.Password != pbConfirmPassword.Password)
                    throw new Exception("Passwords do not match.");

                User nUser = new()
                {
                    Name = tbUsername.Text,
                    Password = pbPassword.Password,
                    IsAdmin = false
                };
                using (UserDbContext context = new())
                {
                    //No UnitOfWork here because connection is to users-dB with encryption, not to recipe-dB
                    UserRepository u = new(context);

                    //Sends user object to repo and returns true if user is successfully created in dB
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
            catch (Exception ex)
            {
                MessageBox.Show("Oops, something went wrong!\n" + ex.Message);
            }
            pbPassword.Clear();
            pbConfirmPassword.Clear();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
