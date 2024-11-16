using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using MyHealthAI.Models;

namespace MyHealthAI
{
    public partial class RegisterView : Window
    {
        private int selectedOption = 10;

        public RegisterView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos de los controles de la interfaz
            string username = txtUsername.Text;
            string password = txtPassword.Password; // Para PasswordBox se usa .Password
            string email = txtEmail.Text;
            string passwordc = txtPasswordC.Password;

            int height = 0;
            if (int.TryParse(txtHeight.Text, out int h))
            {
                height = h;
            }

            double weight = 0;
            if (double.TryParse(txtWeight.Text, out double w))
            {
                weight = w;
            }

            int? goalWeight = null;
            if (int.TryParse(txtGoalWeight.Text, out int g))
            {
                goalWeight = g;
            }

            if (!validate(username, password, email, passwordc, height, weight))
            {
                return;

            }

            // Crear una instancia del modelo User
            User newUser = new User
            {
                Username = username,
                Password = password,
                Email = email,
                Height = height,
                Weight = weight,
                ObjectiveID = selectedOption,
                GoalWeight = goalWeight
            };

            try
            {
                // Crear una instancia del DbContext
                using (var context = new AppDbContext())
                {
                    context.Users.Add(newUser); // Agregar el nuevo usuario
                    context.SaveChanges(); // Guardar cambios en la base de datos
                }

                MessageBox.Show("Usuario guardado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}");

                if (ex.InnerException != null)
                {
                    // Mostrar el mensaje de la InnerException
                    MessageBox.Show($"Inner Exception: {ex.InnerException.Message}");

                    // Mostrar más detalles si es necesario
                    MessageBox.Show($"Inner Exception StackTrace: {ex.InnerException.StackTrace}");
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Verificar cuál RadioButton está seleccionado
            if (RadioButtonA.IsChecked == true)
            {
                selectedOption = 7;  // Asignar valor a la variable
            }
            else if (RadioButtonB.IsChecked == true)
            {
                selectedOption = 8;  // Asignar otro valor
            }
            else if (RadioButtonC.IsChecked == true)
            {
                selectedOption = 9;
            }
        }

        // Método para validar el correo (Gmail)
        private bool IsValidEmail(string email)
        {
            // Expresión regular para validar un correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Verificar que el correo no esté vacío y que cumpla con el patrón de la expresión regular
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, pattern);
        }

        // Método para validar la contraseña (al menos 8 caracteres, una mayúscula y un número)
        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8 &&
                   password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }

        private bool validate(String username, String password, String email, String passwordc, int height, double weight)
        {
            // Validar el nombre de usuario (al menos 3 caracteres)
            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters.");
                return false;
            }

            // Validar la contraseña (debe tener al menos 8 caracteres, una mayúscula y un número)
            if (!IsValidPassword(password))
            {
                MessageBox.Show("The password must have at least 8 characters, a capital letter and a number.");
                return false;
            }

            // Validaciones
            if (password != passwordc)
            {
                MessageBox.Show("Password does not match.");
                return false;
            }

            // Validar el correo (debe ser un correo de Gmail válido)
            if (!IsValidEmail(email))
            {
                MessageBox.Show("The email must be a valid Gmail address.");
                return false;
            }

            // Validar la altura (debe estar entre 0 y 4 metros)
            if (height <= 10 || height > 400)
            {
                MessageBox.Show("The height must be between 10 and 400 cm.");
                return false;
            }

            // Validar el peso (debe estar entre 0 y 1000 kg)
            if (weight <= 0 || weight > 1000)
            {
                MessageBox.Show("The weight must be between 0 and 1000 kg.");
                return false;
            }

            using (var context = new AppDbContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Username == username);
                if (existingUser != null)
                {
                    MessageBox.Show("The username is already in use. Please choose another one.");
                    return false;
                }
            }

            using (var context = new AppDbContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    MessageBox.Show("The email is already registered. Please choose another one.");
                    return false;
                }
            }



            return true;

        }
    }
}
