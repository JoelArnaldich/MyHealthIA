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
using Notifications.Wpf;
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
            var notificationManager = new NotificationManager();
            string email = txtEmail1.Text;
            string password = txtPassword1.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                notificationManager.Show(
                new NotificationContent { Title = "Notification", Message = "Por favor ingrese su nombre de usuario y contraseña."},
                areaName: "WindowArea");
                return;
            }

            LoginAuth loginAuth = new LoginAuth();
            bool isAuthenticated = loginAuth.AuthenticateUser(email, password);

            if (isAuthenticated)
            {

                notificationManager.Show(
                new NotificationContent { Title = "Succes", Message = "Login exitoso.", Type = NotificationType.Success },
                areaName: "WindowArea");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                notificationManager.Show(
                new NotificationContent { Title = "Error", Message = "Usuario o contraseña.", Type = NotificationType.Error },
                areaName: "WindowArea");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

    }
}
