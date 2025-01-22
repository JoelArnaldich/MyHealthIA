
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;

namespace MyHealthAI.Services
{
    public class DailyCalc
    {
        private readonly AppDbContext _dbContext;

        public DailyCalc(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void CalculateDailyNeeds(User user)
        {
            double tmb = CalculateTMB(user.Weight, user.Height, user.Age, user.GenderID);
            double dailyCalories = AdjustCaloriesForActivity(tmb, user.ActivityID);
            dailyCalories = AdjustCaloriesForObjective(dailyCalories, user.ObjectiveID);


            double dailyProteins = dailyCalories * 0.25 / 4;
            double dailyFats = dailyCalories * 0.25 / 9;
            double dailyCarbs = dailyCalories * 0.50 / 4;

            // Calcular la ingesta de agua en mililitros
            double dailyWater = CalculateWaterIntake(user.Weight, user.ActivityID);

            // Guardar resultados en la base de datos
            user.DailyKcal = (int)dailyCalories;
            user.DailyPro = (int)dailyProteins;
            user.DailyFat = (int)dailyFats;
            user.DailyCar = (int)dailyCarbs;
            user.DailyWater = (int)dailyWater; // Guardar agua en mililitros

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
                    return 88.36 + 13.4 * weight.Value + 4.8 * height.Value - 5.7 * age.Value;
                case 2: // Femenino
                    return 447.6 + 9.2 * weight.Value + 3.1 * height.Value - 4.3 * age.Value;
                case 3: // No Binario (media de ambos)
                    double tmbMale = 88.36 + 13.4 * weight.Value + 4.8 * height.Value - 5.7 * age.Value;
                    double tmbFemale = 447.6 + 9.2 * weight.Value + 3.1 * height.Value - 4.3 * age.Value;
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

        // Método para calcular la ingesta diaria de agua en mililitros
        private double CalculateWaterIntake(double? weight, int activityId)
        {
            if (weight == null) return 0;

            // Agua base según el peso (35 ml por kg de peso corporal)
            double waterInMilliliters = weight.Value * 35;

            // Ajuste por nivel de actividad (añadir 500 ml por cada nivel adicional de actividad)
            switch (activityId)
            {
                case 2: waterInMilliliters += 500; break; // Ligeramente Activo
                case 3: waterInMilliliters += 1000; break; // Moderadamente Activo
                case 4: waterInMilliliters += 1500; break; // Muy Activo
                case 5: waterInMilliliters += 2000; break; // Extremadamente Activo
            }

            return waterInMilliliters;
        }

        public async Task<double> CalculateCaloriesWithExercise(User user)
        {
            // Calcular TMB (Tasa Metabólica Basal) de acuerdo con el usuario
            double tmb = CalculateTMB(user.Weight, user.Height, user.Age, user.GenderID);

            // Calcular calorías base según el nivel de actividad del usuario
            double baseCalories = AdjustCaloriesForActivity(tmb, user.ActivityID);

            // Obtener el ejercicio registrado para el día actual
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var exercises = await _dbContext.Exercises
                .Where(e => e.UserID == user.ID && e.Date == today)
                .ToListAsync();

            // Calcular calorías adicionales quemadas con ejercicio
            double exerciseCalories = 0;
            foreach (var exercise in exercises)
            {
                exerciseCalories += exercise.CaloriesBurned;
            }

            // Sumar calorías base y calorías adicionales por ejercicio
            double totalCalories = baseCalories + exerciseCalories;

            return totalCalories;
        }

    }
}
