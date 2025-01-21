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

        public (bool, String) validate(String username, String password, String passwordc,String email, int? height, double? weight, int ObjectiveID, int ActivityID, int GenderID , double? goalWeight, int? age)
        {
            String message = "";
           

            if (username == null)
            {
               message  = "El nombre de usuario ha de ser entre 3 i 15 caracteres";
                return (false,message);
            }

            else if (username.Length > 15 || username.Length < 3)
            {
                message = "El nombre de usuario ha de ser entre 3 i 15 caracteres.";
                return (false, message);
            }


            if (!IsValidPassword(password))
            {
                message = "The password must have at least 8 characters, a capital letter and a number.";
                return (false, message);
            }


            if (password != passwordc)
            {
                message = "Password does not match.";
                return (false, message);
            }


            if (!IsValidEmail(email))
            {
                message = "The email must be a valid Gmail address.";
                return (false, message);
            }

            if (!height.HasValue || height < 10 || height > 400)
                return (false, "La altura debe estar entre 10 cm y 400 cm.");

            if (!weight.HasValue || weight < 10 || weight > 1000)
                return (false, "El peso debe estar entre 10 kg y 1000 kg.");


            if (!age.HasValue || age < 10 || age > 200)
                return (false, "La edad deve de estar entre 10 y 200.");


            if (!goalWeight.HasValue || goalWeight < 10 || goalWeight > 1000)
                return (false, "El peso objetivo debe estar entre 10 kg y 1000 kg.");


            if (ObjectiveID == 0)
            {
                return (false, "Selecciona un objetivo");
            }

            if (ActivityID == 0)
            {
                return (false, "Seleciona una actividad diaria");
            }
            if (GenderID == 0)
            {
                return (false, "Seleciona un genero");
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
