using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
public class CalorieService
{
    private readonly AppDbContext _dbContext;

    public CalorieService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<(int TotalCalories, int TotalFat, int TotalProteins, int TotalCarbs, int TotalWater)>
        GetDailyNutrientIntakeAsync(int userID)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);


        var result = await _dbContext.Meals
            .Where(m => m.UserID == userID && m.MealDate == today)
            .GroupBy(m => 1)
            .Select(g => new
            {
                TotalCalories = g.Sum(m => m.Kcal),
                TotalFat = g.Sum(m => m.Fat),
                TotalProteins = g.Sum(m => m.Protein),
                TotalCarbs = g.Sum(m => m.Carbohydrate)
            })
            .FirstOrDefaultAsync();


        var waterResult = await _dbContext.Water
            .Where(w => w.UserID == userID && w.Date == today)
            .SumAsync(w => (int?)w.WaterMl) ?? 0; 

        if (result == null)
        {

            return (0, 0, 0, 0, waterResult);
        }

        return (result.TotalCalories, result.TotalFat, result.TotalProteins, result.TotalCarbs, waterResult);
    }
}
