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

        bool register = false;

        public LoginView()
        {


            InitializeComponent();

        }
        private void ChLogin_Change(object sender, EventArgs e)
        {
            register = true;
            RegisterView registerView = new RegisterView();
            this.Close();
            registerView.Show();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail1.Text;
            string password = txtPassword1.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese su nombre de usuario y contraseña");
                return;
            }

            LoginAuth loginAuth = new LoginAuth();
            bool isAuthenticated = loginAuth.AuthenticateUser(email, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login exitoso");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Correo electronico o contraseña incorrectos");
            }


        }

        protected override void OnClosed(EventArgs e)
        {
            if (!register)
            {
                
                base.OnClosed(e);
                Application.Current.Shutdown();
            }
        }

    }
}