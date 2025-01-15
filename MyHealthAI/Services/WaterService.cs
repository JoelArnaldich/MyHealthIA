using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // Obtener la fecha de hoy como DateOnly
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Asegurar que la semana comienza el lunes y termina el domingo
            DateOnly startOfWeek = today.AddDays(-((int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1)); // Calcular el lunes
            DateOnly endOfWeek = startOfWeek.AddDays(6); // Calcular el domingo

            // Consultar la base de datos para obtener el agua consumida en la semana actual
            var waterData = await _dbContext.Water
                .Where(w => w.UserID == userId && w.Date >= startOfWeek && w.Date <= endOfWeek)
                .GroupBy(w => w.Date) // Agrupar por fecha
                .Select(g => new WaterDayData
                {
                    Date = g.Key,
                    TotalWaterMl = g.Sum(w => w.WaterMl)
                })
                .OrderBy(w => w.Date) // Ordenar por fecha
                .ToListAsync();

            // Si no hay datos para algunos días de la semana, los añadimos manualmente con valores 0
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

    // Clase para almacenar los datos de consumo de agua por día
    public class WaterDayData
    {
        public DateOnly Date { get; set; }
        public int TotalWaterMl { get; set; }
    }
}
