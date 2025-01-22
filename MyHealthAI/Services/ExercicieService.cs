using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;

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
    
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);


            DateOnly startOfWeek = today.AddDays(-((int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1)); 
            DateOnly endOfWeek = startOfWeek.AddDays(6); 

            var exercises = await _dbContext.Exercises
                .Where(e => e.UserID == userId && e.Date >= startOfWeek && e.Date <= endOfWeek)
                .GroupBy(e => e.Date) 
                .Select(g => new ExerciseDayData
                {
                    Date = g.Key,
                    TotalCaloriesBurned = g.Sum(e => e.CaloriesBurned),
                    TotalMinutesTrained = g.Sum(e => e.DurationInMinutes)
                })
                .OrderBy(e => e.Date) 
                .ToListAsync();


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


    public class ExerciseDayData
    {
        public DateOnly Date { get; set; }
        public double TotalCaloriesBurned { get; set; }
        public double TotalMinutesTrained { get; set; }
    }
}
