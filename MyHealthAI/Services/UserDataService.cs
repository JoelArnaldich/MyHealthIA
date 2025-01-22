
using System.Text;
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
    
            if (userId <= 0)
            {
                return "ID de usuario inválido.";
            }


            var userData = await (from u in _dbContext.Users
                                  where u.ID == userId
                                  select new
                                  {
                                      User = u,
                                      Meals = _dbContext.Meals.Where(m => m.UserID == u.ID),
                                      Exercises = _dbContext.Exercises.Where(e => e.UserID == u.ID),
                                      Water = _dbContext.Water.Where(w => w.UserID == u.ID)
                                  }).FirstOrDefaultAsync();


            if (userData == null)
            {
                return "No se encontraron datos para este usuario.";
            }


            StringBuilder sb = new StringBuilder();

 
            sb.AppendLine($"Usuario: {userData.User.Name}");
            sb.AppendLine($"Edad: {userData.User.Age}");
            sb.AppendLine($"Peso: {userData.User.Weight} kg");
            sb.AppendLine($"Altura: {userData.User.Height} cm");


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
