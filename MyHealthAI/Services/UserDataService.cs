using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;

namespace MyHealthAI.Services
{
    public class UserDataService
    {
        private readonly AppDbContext _dbContext;

        public UserDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetUserDataByIdAsync(int userId)
        {
            // Verificar si el ID del usuario es válido
            if (userId <= 0)
            {
                return "ID de usuario inválido.";
            }

            // Obtener los datos del usuario y las tablas relacionadas
            var userData = await (from u in _dbContext.Users
                                  where u.ID == userId
                                  select new
                                  {
                                      User = u,
                                      Meals = _dbContext.Meals.Where(m => m.UserID == u.ID),
                                      Exercises = _dbContext.Exercises.Where(e => e.UserID == u.ID),
                                      Water = _dbContext.Water.Where(w => w.UserID == u.ID)
                                  }).FirstOrDefaultAsync();

            // Verificar si se encontraron datos para el usuario
            if (userData == null)
            {
                return "No se encontraron datos para este usuario.";
            }

            // Construir el string con los datos del usuario
            StringBuilder sb = new StringBuilder();

            // Datos del usuario
            sb.AppendLine($"Usuario: {userData.User.Name}");
            sb.AppendLine($"Edad: {userData.User.Age}");
            sb.AppendLine($"Peso: {userData.User.Weight} kg");
            sb.AppendLine($"Altura: {userData.User.Height} cm");

            // Comidas del usuario
            if (userData.Meals.Any())
            {
                sb.AppendLine("Comidas:");
                foreach (var meal in userData.Meals)
                {
                    sb.AppendLine($"- {meal.Name}: {meal.Kcal} kcal, {meal.Protein}g proteína, {meal.Carbohydrate}g carbohidrato, {meal.Fat}g grasa");
                }
            }
            else
            {
                sb.AppendLine("No se encontraron comidas registradas.");
            }

            // Ejercicio del usuario
            if (userData.Exercises.Any())
            {
                sb.AppendLine("Ejercicio:");
                foreach (var exercise in userData.Exercises)
                {
                    sb.AppendLine($"- {exercise.ExerciseType}: {exercise.DurationInMinutes} min, {exercise.CaloriesBurned} kcal quemadas");
                }
            }
            else
            {
                sb.AppendLine("No se encontraron registros de ejercicios.");
            }

            // Agua del usuario
            if (userData.Water.Any())
            {
                sb.AppendLine("Agua:");
                foreach (var waterRecord in userData.Water)
                {
                    sb.AppendLine($"- {waterRecord.Date}: {waterRecord.WaterMl} ml de agua");
                }
            }
            else
            {
                sb.AppendLine("No se encontraron registros de agua.");
            }

            return sb.ToString();
        }
    }
}
