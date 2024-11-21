using MyHealthAI.Migrations;
using MyHealthAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MyHealthAI.Services
{
    public class RegisterAuth
    {
        public void register(String username, String password,String email,int height,double weight,int selectedOption,int? goalWeight)
        {


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


            }
            catch (Exception ex)
            {


            }

        }

        // Método para validar el correo (Gmail)
        public bool IsValidEmail(string email)
        {
            // Expresión regular para validar un correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Verificar que el correo no esté vacío y que cumpla con el patrón de la expresión regular
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, pattern);
        }

        // Método para validar la contraseña (al menos 8 caracteres, una mayúscula y un número)
        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8 &&
                   password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }

        public (bool, String) validate(String username, String password, String passwordc,String email, int height, double weight)
        {
            String message = "";
            // Validar el nombre de usuario (al menos 3 caracteres)
            if (username.Length < 3)
            {
               message  = "Username must be at least 3 characters.";
                return (false,message);
            }

            // Validar la contraseña (debe tener al menos 8 caracteres, una mayúscula y un número)
            if (!IsValidPassword(password))
            {
                message = "The password must have at least 8 characters, a capital letter and a number.";
                return (false, message);
            }

            // Validaciones
            if (password != passwordc)
            {
                message = "Password does not match.";
                return (false, message);
            }

            // Validar el correo (debe ser un correo de Gmail válido)
            if (!IsValidEmail(email))
            {
                message = "The email must be a valid Gmail address.";
                return (false, message);
            }

            // Validar la altura (debe estar entre 0 y 4 metros)
            if (height <= 10 || height > 400)
            {
                message = "The height must be between 10 and 400 cm.";
                return (false, message);
            }

            // Validar el peso (debe estar entre 0 y 1000 kg)
            if (weight <= 0 || weight > 1000)
            {
                message = "The weight must be between 0 and 1000 kg.";
                return (false, message);
            }

            using (var context = new AppDbContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    message = "The email is already registered. Please choose another one.";
                    return (false, message);
                }
            }



            return (true, message);

        }
    }
}
