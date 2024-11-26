using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
public class CalorieService
{
    private readonly AppDbContext _dbContext;

    public CalorieService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Función para obtener los totales de nutrientes de un usuario en el día actual
    public async Task<(int TotalCalories, int TotalFat, int TotalProteins, int TotalCarbs)>
        GetDailyNutrientIntakeAsync(int userID)
    {
        // Obtener la fecha de hoy (solo la parte de la fecha)
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        // Obtener las sumas directamente desde la base de datos
        var result = await _dbContext.Meals
            .Where(m => m.UserID == userID && m.MealDate == today)
            .GroupBy(m => 1) // No nos importa la agrupación, solo sumamos todo
            .Select(g => new
            {
                TotalCalories = g.Sum(m => m.Kcal),
                TotalFat = g.Sum(m => m.Fat),
                TotalProteins = g.Sum(m => m.Protein),
                TotalCarbs = g.Sum(m => m.Carbohydrate)
            })
            .FirstOrDefaultAsync();

        // Si no se encuentran comidas, asignar valores por defecto (0)
        if (result == null)
        {
            return (0, 0, 0, 0);
        }

        return (result.TotalCalories, result.TotalFat, result.TotalProteins, result.TotalCarbs);
    }
}
