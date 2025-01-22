using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyHealthAI.Services
{
    public class WaterService
    {
        private readonly AppDbContext _dbContext;

        public WaterService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WaterDayData>> GetWaterDataForWeekAsync(int userId)
        {

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);


            DateOnly startOfWeek = today.AddDays(-((int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1)); 
            DateOnly endOfWeek = startOfWeek.AddDays(6); 


            var waterData = await _dbContext.Water
                .Where(w => w.UserID == userId && w.Date >= startOfWeek && w.Date <= endOfWeek)
                .GroupBy(w => w.Date) 
                .Select(g => new WaterDayData
                {
                    Date = g.Key,
                    TotalWaterMl = g.Sum(w => w.WaterMl)
                })
                .OrderBy(w => w.Date) 
                .ToListAsync();

            List<WaterDayData> allDays = new List<WaterDayData>();
            for (int i = 0; i < 7; i++)
            {
                var date = startOfWeek.AddDays(i);
                var dayData = waterData.FirstOrDefault(w => w.Date == date);
                if (dayData == null)
                {
                    dayData = new WaterDayData
                    {
                        Date = date,
                        TotalWaterMl = 0
                    };
                }
                allDays.Add(dayData);
            }

            return allDays;
        }
    }

    public class WaterDayData
    {
        public DateOnly Date { get; set; }
        public int TotalWaterMl { get; set; }
    }
}
