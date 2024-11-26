using System;
using System.Linq;
using MyHealthAI.Models;

namespace MyHealthAI.ViewModels
{
    public class DailyCalc
    {
        private readonly AppDbContext _dbContext;

        public DailyCalc(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para calcular calorías diarias y macronutrientes
        public void CalculateDailyNeeds(User user)
        {
            double tmb = CalculateTMB(user.Weight, user.Height, user.Age, user.GenderID);
            double dailyCalories = AdjustCaloriesForActivity(tmb, user.ActivityID);
            dailyCalories = AdjustCaloriesForObjective(dailyCalories, user.ObjectiveID);

            // Calcular macronutrientes
            double dailyProteins = (dailyCalories * 0.25) / 4; // gramos de proteína
            double dailyFats = (dailyCalories * 0.25) / 9; // gramos de grasa
            double dailyCarbs = (dailyCalories * 0.50) / 4; // gramos de carbohidratos

            // Guardar resultados en la base de datos
            user.DailyKcal = (int)dailyCalories;
            user.DailyPro = (int)dailyProteins;
            user.DailyFat = (int)dailyFats;
            user.DailyCar = (int)dailyCarbs;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        // Método para calcular la TMB
        private double CalculateTMB(double? weight, int? height, int? age, int genderId)
        {
            if (weight == null || height == null || age == null) return 0;

            switch (genderId)
            {
                case 1: // Masculino
                    return 88.36 + (13.4 * weight.Value) + (4.8 * height.Value) - (5.7 * age.Value);
                case 2: // Femenino
                    return 447.6 + (9.2 * weight.Value) + (3.1 * height.Value) - (4.3 * age.Value);
                case 3: // No Binario (media de ambos)
                    double tmbMale = 88.36 + (13.4 * weight.Value) + (4.8 * height.Value) - (5.7 * age.Value);
                    double tmbFemale = 447.6 + (9.2 * weight.Value) + (3.1 * height.Value) - (4.3 * age.Value);
                    return (tmbMale + tmbFemale) / 2;
                default:
                    return 0;
            }
        }

        // Método para ajustar por nivel de actividad
        private double AdjustCaloriesForActivity(double tmb, int activityId)
        {
            switch (activityId)
            {
                case 1: return tmb * 1.2;   // Sedentario
                case 2: return tmb * 1.375; // Ligeramente Activo
                case 3: return tmb * 1.55;  // Moderadamente Activo
                case 4: return tmb * 1.725; // Muy Activo
                case 5: return tmb * 1.9;   // Extremadamente Activo
                default: return tmb;
            }
        }

        // Método para ajustar por objetivo
        private double AdjustCaloriesForObjective(double calories, int objectiveId)
        {
            switch (objectiveId)
            {
                case 1: return calories * 0.8;  // Perder Peso
                case 2: return calories * 1.15; // Ganar Peso y Músculo
                case 3: return calories * 0.9;  // Perder Peso y Ganar Músculo
                case 4: return calories;        // Mantenimiento
                default: return calories;
            }
        }
    }
}
