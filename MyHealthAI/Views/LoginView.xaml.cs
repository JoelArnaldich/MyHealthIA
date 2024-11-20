using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Windows;
using MyHealthAI.Models;
using MyHealthAI.Services;
namespace MyHealthAI
{
    public partial class LoginView : Window
    {
        public LoginView(){

        
            InitializeComponent();

        }
        private void ChLogin_Change(object sender, EventArgs e)
        {
                RegisterView registerView = new RegisterView();
                this.Close();
                registerView.Show();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername1.Text;
            string password = txtPassword1.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese su nombre de usuario y contraseña.");
                return;
            }

            LoginAuth loginAuth = new LoginAuth();
            bool isAuthenticated = loginAuth.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login exitoso.");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.");
            }
        }

    }
}
