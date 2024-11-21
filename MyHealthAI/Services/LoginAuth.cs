using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHealthAI.Models;

namespace MyHealthAI.Services
{
    public class LoginAuth
    {

        public bool AuthenticateUser(string email, string password)
        {
            using (var context = new AppDbContext())
            {

                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);


                if (user != null)
                {

                    CurrentUser.LoggedInUserId = user.ID;
                    return true;
                }

                return false;
            }
        
        }

    }
}
