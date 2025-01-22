
using MyHealthAI.Models;
using System.Text.RegularExpressions;



namespace MyHealthAI.Services
{
    public class RegisterAuth
    {
     

        public bool IsValidEmail(string email)
        {

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";


            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, pattern);
        }


        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8 &&
                   password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }

        public (bool, String) validate(String username, String password, String passwordc, String email, int? height, double? weight, int ObjectiveID, int ActivityID, int GenderID, double? goalWeight, int? age)
        {
            String mensaje = "";

            if (username == null)
            {
                mensaje = "El nombre de usuario debe tener entre 3 y 15 caracteres.";
                return (false, mensaje);
            }
            else if (username.Length > 15 || username.Length < 3)
            {
                mensaje = "El nombre de usuario debe tener entre 3 y 15 caracteres.";
                return (false, mensaje);
            }

            if (!IsValidPassword(password))
            {
                mensaje = "La contraseña debe tener al menos 8 caracteres, una letra mayúscula y un número.";
                return (false, mensaje);
            }

            if (password != passwordc)
            {
                mensaje = "Las contraseñas no coinciden.";
                return (false, mensaje);
            }

            if (!IsValidEmail(email))
            {
                mensaje = "El correo electrónico debe ser una dirección de Gmail válida.";
                return (false, mensaje);
            }

            if (!height.HasValue || height < 10 || height > 400)
                return (false, "La altura debe estar entre 10 cm y 400 cm.");

            if (!weight.HasValue || weight < 10 || weight > 1000)
                return (false, "El peso debe estar entre 10 kg y 1000 kg.");

            if (!age.HasValue || age < 10 || age > 200)
                return (false, "La edad debe estar entre 10 y 200 años.");

            if (!goalWeight.HasValue || goalWeight < 10 || goalWeight > 1000)
                return (false, "El peso objetivo debe estar entre 10 kg y 1000 kg.");

            if (ObjectiveID == 0)
            {
                return (false, "Selecciona un objetivo.");
            }

            if (ActivityID == 0)
            {
                return (false, "Selecciona una actividad diaria.");
            }

            if (GenderID == 0)
            {
                return (false, "Selecciona un género.");
            }

            using (var context = new AppDbContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    mensaje = "El correo electrónico ya está registrado. Por favor, elige otro.";
                    return (false, mensaje);
                }
            }

            return (true, mensaje);
        }

    }
}
