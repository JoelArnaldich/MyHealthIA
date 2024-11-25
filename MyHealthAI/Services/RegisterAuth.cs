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



            User newUser = new User
            {
                Username = username,
                Password = password,
                Email = email,
                Height = height,
                Weight = weight,
                ObjectiveID = selectedOption,
                GoalWeight = goalWeight,
                Age = 0,
                ActivityID = 0,
                GenderID = 0,  
            };

            try
            {

                using (var context = new AppDbContext())
                {
                    context.Users.Add(newUser); 
                    context.SaveChanges(); 
                }


            }
            catch (Exception ex)
            {


            }

        }

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

        public (bool, String) validate(String username, String password, String passwordc,String email, int height, double weight)
        {
            String message = "";

            if (username.Length < 3)
            {
               message  = "Username must be at least 3 characters.";
                return (false,message);
            }

            if (username.Length > 15)
            {
                message = "El nombre de usuario ha de ser de menos de 16 caracteres.";
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


            if (height <= 10 || height > 400)
            {
                message = "The height must be between 10 and 400 cm.";
                return (false, message);
            }


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
