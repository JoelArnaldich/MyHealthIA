using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHealthAI.Services
{
    public class ExerciseService
    {
        private readonly AppDbContext _dbContext;

        public ExerciseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ExerciseDayData>> GetExerciseDataForWeekAsync(int userId)
        {
            // Obtener la fecha de hoy como DateOnly
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Calcular el primer día de la semana (lunes) y el último (domingo)
            DateOnly startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday); // Primer día de la semana
            DateOnly endOfWeek = startOfWeek.AddDays(6); // Último día de la semana (domingo)

            // Consultar la base de datos para obtener los ejercicios de la semana actual
            var exercises = await _dbContext.Exercises
                .Where(e => e.UserID == userId && e.Date >= startOfWeek && e.Date <= endOfWeek)
                .GroupBy(e => e.Date) // Agrupar por fecha
                .Select(g => new ExerciseDayData
                {
                    Date = g.Key,
                    TotalCaloriesBurned = g.Sum(e => e.CaloriesBurned),
                    TotalMinutesTrained = g.Sum(e => e.DurationInMinutes)
                })
                .OrderBy(e => e.Date) // Ordenar por fecha
                .ToListAsync();

            // Si no hay datos para algunos días de la semana, los añadimos manualmente con valores 0
            List<ExerciseDayData> allDays = new List<ExerciseDayData>();
            for (int i = 0; i < 7; i++)
            {
                var date = startOfWeek.AddDays(i);
                var dayData = exercises.FirstOrDefault(e => e.Date == date);
                if (dayData == null)
                {
                    dayData = new ExerciseDayData
                    {
                        Date = date,
                        TotalCaloriesBurned = 0,
                        TotalMinutesTrained = 0
                    };
                }
                allDays.Add(dayData);
            }

            return allDays;
        }
    }

    // Clase para almacenar los datos de ejercicios por día
    public class ExerciseDayData
    {
        public DateOnly Date { get; set; }
        public double TotalCaloriesBurned { get; set; }
        public double TotalMinutesTrained { get; set; }
    }
}
