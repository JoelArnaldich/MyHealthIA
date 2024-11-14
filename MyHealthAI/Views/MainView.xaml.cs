using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using MyHealthAI.Models;
using System;
using System.Windows;

namespace MyHealthAI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos de los controles de la interfaz
            string username = txtUsername.Text;
            string password = txtPassword.Password; // Para PasswordBox se usa .Password
            string email = txtEmail.Text;

            int? height = null;
            if (int.TryParse(txtHeight.Text, out int h))
            {
                height = h;
            }

            int? weight = null;
            if (int.TryParse(txtWeight.Text, out int w))
            {
                weight = w;
            }

            int? objectiveID = null;
            if (int.TryParse(txtObjective.Text, out int o))
            {
                objectiveID = o;
            }


            int? goalWeight = null;
            if (int.TryParse(txtGoalWeight.Text, out int g))
            {
                goalWeight = g;
            }

            // Crear una instancia del modelo User
            User newUser = new User
            {
                Username = username,
                Password = password,
                Email = email,
                Height = height,
                Weight = weight,
                ObjectiveID = objectiveID,
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

        private void btnShowUsers_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                // Obtener todos los usuarios
                var users = context.Users.ToList();

                // Mostrar los usuarios en un mensaje
                StringBuilder sb = new StringBuilder();
                foreach (var user in users)
                {
                    sb.AppendLine($"ID: {user.ID}, Username: {user.Username}, Email: {user.Email}");
                }

                MessageBox.Show(sb.ToString());
            }
        }

    }
}
