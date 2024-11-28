using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
public class CalorieService
{
    private readonly AppDbContext _dbContext;

    public CalorieService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Método para obtener las calorías, grasas, proteínas, carbohidratos y agua consumida en el día actual
    public async Task<(int TotalCalories, int TotalFat, int TotalProteins, int TotalCarbs, int TotalWater)>
        GetDailyNutrientIntakeAsync(int userID)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        // Obtener los totales de las comidas para el día actual
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

        // Obtener el total de agua consumida en el día actual
        var waterResult = await _dbContext.Water
            .Where(w => w.UserID == userID && w.Date == today)
            .SumAsync(w => (int?)w.WaterMl) ?? 0; // Si no hay datos, asignar 0

        if (result == null)
        {
            // Si no se encuentran comidas para el día, retornar 0s
            return (0, 0, 0, 0, waterResult);
        }

        // Retornar los totales, incluyendo el agua
        return (result.TotalCalories, result.TotalFat, result.TotalProteins, result.TotalCarbs, waterResult);
    }
}
