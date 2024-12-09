using System;
using System.Linq;
using System.Windows.Input;
using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using MyHealthAI.Services;
using System.Windows;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

namespace MyHealthAI.ViewModels
{
    public class MealsPopUpViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext;
        private readonly CalorieService _calorieService;
        private List<Meal> _mealsByType;
        private MealType _selectedMealType;
        private Meal _selectedMeal;
        private readonly HomeViewModel _homeViewModel;
        private List<ISeries> _series;
        private List<ISeries> _series1;
        private List<ISeries> _series2;
        private List<ISeries> _series3;

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
                if (SetProperty(ref _selectedMealType, value ?? MealTypes.FirstOrDefault(mt => mt.ID == 0)))
                {
                    FilterMealsByType(_selectedMealType);
                }
            }
        }
        public Meal SelectedMeal
        {
            get => _selectedMeal;
            set => SetProperty(ref _selectedMeal, value);
        }
        public List<MealType> MealTypes { get; set; }
        public ICommand AddMealCommand { get; }
        public List<ISeries> Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(); // Notificar cambios al UI
            }
        }
        public List<ISeries> Series1
        {
            get => _series1;
            set
            {
                _series1 = value;
                OnPropertyChanged(); // Notificar cambios al UI
            }
        }
        public List<ISeries> Series2
        {
            get => _series2;
            set
            {
                _series2 = value;
                OnPropertyChanged(); // Notificar cambios al UI
            }
        }
        public List<ISeries> Series3
        {
            get => _series3;
            set
            {
                _series3 = value;
                OnPropertyChanged(); // Notificar cambios al UI
            }
        }


        public MealsPopUpViewModel(AppDbContext dbContext, CalorieService calorieService , HomeViewModel homeViewModel)
        {
            _dbContext = dbContext;
            _calorieService = calorieService;
            _homeViewModel = homeViewModel;
 
            LoadMealTypes();
            SelectedMealType = MealTypes.FirstOrDefault(mt => mt.ID == 0);
            LoadMeals();

            AddMealCommand = new RelayCommand(AddSelectedMeal, CanAddMeal);
        }

        private void LoadMeals()
        {
            var userId = CurrentUser.LoggedInUserId;

            var meals = _dbContext.Meals
                .Where(m => m.UserID == userId)
                .ToList();

            var distinctMeals = meals
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

            MealsByType = distinctMeals;
        }

        private void LoadMealTypes()
        {
            MealTypes = _dbContext.MealType.ToList();
            MealTypes.Insert(0, new MealType { ID = 0, Name = "Todas las comidas" });
        }

        private void FilterMealsByType(MealType selectedType)
        {
            var userId = CurrentUser.LoggedInUserId;

            if (selectedType == null || selectedType.ID == 0)
            {
                MealsByType = _dbContext.Meals
                    .Where(m => m.UserID == userId)
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
            else
            {
                MealsByType = _dbContext.Meals
                    .Where(m => m.MealTypeID == selectedType.ID && m.UserID == userId)
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

        private bool CanAddMeal(object parameter)
        {
            return SelectedMeal != null;
        }

        private async void AddSelectedMeal(object parameter)
        {
            if (SelectedMeal == null) return;

            try
            {
                var newMeal = new Meal
                {
                    Name = SelectedMeal.Name,
                    Kcal = SelectedMeal.Kcal,
                    Protein = SelectedMeal.Protein,
                    Carbohydrate = SelectedMeal.Carbohydrate,
                    Fat = SelectedMeal.Fat,
                    Weight = SelectedMeal.Weight,
                    MealDate = DateOnly.FromDateTime(DateTime.Today),
                    MealTypeID = SelectedMeal.MealTypeID,
                    UserID = CurrentUser.LoggedInUserId
                };

                _dbContext.Meals.Add(newMeal);
                _dbContext.SaveChanges();

                
           

                MessageBox.Show("La comida ha sido añadida como consumida hoy.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al añadir la comida: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
