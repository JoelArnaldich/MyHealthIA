using System;
using System.Linq;
using System.Windows.Input;
using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyHealthAI.Services;
using System.Windows.Documents;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Diagnostics.Eventing.Reader;
using LiveChartsCore.SkiaSharpView.VisualElements;
using System.Windows;

namespace MyHealthAI.ViewModels
{
    public class MealsPopUpViewModel : BaseViewModel
    {

        private readonly AppDbContext _dbContext;
        private readonly CalorieService _calorieService;
        private List<Meal> _mealsByType;
        private MealType _selectedMealType;


        public List<Meal> MealsByType
        {
            get => _mealsByType;
            set => SetProperty(ref _mealsByType, value);
        }

        public MealType SelectedMealType
        {
            get => _selectedMealType;
            set
            {
                if (SetProperty(ref _selectedMealType, value))
                {
                    FilterMealsByType(value); 
                }
            }
        }


        public List<MealType> MealTypes { get; set; }

        public MealsPopUpViewModel(AppDbContext dbContext, CalorieService calorieService)
        {
        
            _dbContext = dbContext;
            _calorieService = calorieService;
            LoadMeals();
            LoadMealTypes();

        }
        private void LoadMeals()
        {
            // Obtener las comidas del usuario logueado
            var userId = CurrentUser.LoggedInUserId;

            var meals = _dbContext.Meals
                .Where(m => m.UserID == userId)
                .ToList();

            // Eliminar comidas duplicadas que tengan el mismo nombre, calorías, proteínas, grasas y carbohidratos
            var distinctMeals = meals
                .GroupBy(m => new
                {
                    m.Name,
                    m.Kcal,
                    m.Protein,
                    m.Carbohydrate,
                    m.Fat
                }) // Agrupar por nombre, calorías, proteínas, grasas y carbohidratos
                .Select(g => g.FirstOrDefault()) // Seleccionar el primer registro de cada grupo (eliminando duplicados)
                .ToList();

            MealsByType = distinctMeals;
        }
        private void LoadMealTypes()
        {
            // Obtener los tipos de comidas desde la base de datos
            MealTypes = _dbContext.MealType.ToList();
        }

        private void FilterMealsByType(MealType selectedType)
        {
            if (selectedType == null || selectedType.ID == 0)
            {
                LoadMeals(); // Si no hay tipo seleccionado, mostrar todas las comidas

            }
            else
            {
                // Filtrar las comidas por el tipo seleccionado
                MealsByType = _dbContext.Meals
                    .Where(m => m.MealTypeID == selectedType.ID && m.UserID == CurrentUser.LoggedInUserId)
                    .GroupBy(m => new
                    {
                        m.Name,
                        m.Kcal,
                        m.Protein,
                        m.Carbohydrate,
                        m.Fat
                    })
                    .Select(g => g.FirstOrDefault())
                    .ToList();
            }
        }
    }

}
