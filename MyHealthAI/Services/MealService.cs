using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;

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

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);


            DateOnly startOfWeek = today.AddDays(-((int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1)); // Calcular el lunes
            DateOnly endOfWeek = startOfWeek.AddDays(6); // Calcular el domingo


            var meals = await _dbContext.Meals
                .Where(m => m.UserID == userId && m.MealDate >= startOfWeek && m.MealDate <= endOfWeek)
                .GroupBy(m => m.MealDate) 
                .Select(g => new MealDayData
                {
                    Date = g.Key,
                    Kcal = g.Sum(m => m.Kcal) 
                })
                .OrderBy(m => m.Date) 
                .ToListAsync();


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
