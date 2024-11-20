using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Windows;
using MyHealthAI.Models;

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
            // Obtener el nombre de usuario y la contraseña ingresados
            string username = txtUsername1.Text;
            string password = txtPassword1.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese su nombre de usuario y contraseña.");
                return;
            }

            // Llamada al método de autenticación
            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login exitoso.");
                // Redirigir a la siguiente ventana o funcionalidad (ejemplo: MainView)
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.");
            }
        }

        // Método que valida si el nombre de usuario y la contraseña son correctos
        private bool AuthenticateUser(string username, string password)
        {
            using (var context = new AppDbContext())
            {
                // Verifica si existe un usuario con el nombre de usuario y contraseña proporcionados
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                // Si el usuario no existe o las credenciales no coinciden, retornamos false
                return user != null;
            }
        }
    }
}
