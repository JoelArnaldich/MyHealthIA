using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHealthAI.Services
{
    public class MealService
    {
        private readonly AppDbContext _dbContext;

        public MealService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MealDayData>> GetMealDataForWeekAsync(int userId)
        {
            // Obtener la fecha de hoy como DateOnly
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Asegurar que la semana comienza el lunes y termina el domingo
            DateOnly startOfWeek = today.AddDays(-((int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1)); // Calcular el lunes
            DateOnly endOfWeek = startOfWeek.AddDays(6); // Calcular el domingo

            // Consultar la base de datos para obtener las comidas consumidas durante la semana actual
            var meals = await _dbContext.Meals
                .Where(m => m.UserID == userId && m.MealDate >= startOfWeek && m.MealDate <= endOfWeek)
                .GroupBy(m => m.MealDate) // Agrupar por fecha
                .Select(g => new MealDayData
                {
                    Date = g.Key,
                    Kcal = g.Sum(m => m.Kcal) // Sumar las calorías de las comidas
                })
                .OrderBy(m => m.Date) // Ordenar por fecha
                .ToListAsync();

            // Si no hay datos para algunos días de la semana, los añadimos manualmente con valores 0
            List<MealDayData> allDays = new List<MealDayData>();
            for (int i = 0; i < 7; i++)
            {
                var date = startOfWeek.AddDays(i);
                var dayData = meals.FirstOrDefault(m => m.Date == date);
                if (dayData == null)
                {
                    dayData = new MealDayData
                    {
                        Date = date,
                        Kcal = 0
                    };
                }
                allDays.Add(dayData);
            }

            return allDays;
        }
    }

    public class MealDayData
    {
        public DateOnly Date { get; set; }
        public int Kcal { get; set; }
    }
}
