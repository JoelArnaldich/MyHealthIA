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
                // Verifica si existe un usuario con el nombre de usuario y contraseña proporcionados
                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                // Si el usuario no existe o las credenciales no coinciden, retornamos false
                return user != null;
            }
        }

    }
}
