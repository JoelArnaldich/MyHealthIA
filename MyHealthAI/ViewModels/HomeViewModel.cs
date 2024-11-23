using System;
using System.Linq;
using System.Windows.Input;
using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace MyHealthAI.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext;
        private string _message;
        private string _mealName;
        private int? _mealKcal;
        private int? _mealWeight;
        private int? _mealProtein;
        private int? _mealCarbohydrate;
        private int? _mealFat;
        private int _selectedMealTypeId;
        private string _statusMessage;

        // ObservableCollection para enlazar con ComboBox
        public ObservableCollection<MealType> MealType { get; set; }


        // Propiedades para enlazar con la Vista (Formulario)
        public string MealName
        {
            get => _mealName;
            set => SetProperty(ref _mealName, value);
        }

        public int? MealKcal
        {
            get => _mealKcal;
            set => SetProperty(ref _mealKcal, value);
        }

        public int? MealWeight
        {
            get => _mealWeight;
            set => SetProperty(ref _mealWeight, value);
        }

        public int? MealProtein
        {
            get => _mealProtein;
            set => SetProperty(ref _mealProtein, value);
        }

        public int? MealCarbohydrate
        {
            get => _mealCarbohydrate;
            set => SetProperty(ref _mealCarbohydrate, value);
        }

        public int? MealFat
        {
            get => _mealFat;
            set => SetProperty(ref _mealFat, value);
        }


        public int SelectedMealTypeId
        {
            get => _selectedMealTypeId;
            set => SetProperty(ref _selectedMealTypeId, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }



        // Comando para guardar la comida
        public ICommand SaveMealCommand { get; }
        public string Message { get; private set; }

        public HomeViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
            MealType = new ObservableCollection<MealType>(_dbContext.MealType.ToList()); // Cargar MealTypes desde la base de datos
            MealType.Insert(0, new MealType { ID = 0, Name = "Seleccione el tipo de comida" });
            SetWelcomeMessage();

            // Inicializar el comando
            SaveMealCommand = new RelayCommand(SaveMeal, CanSaveMeal);
        }

        // Lógica para guardar la comida
        private async void SaveMeal(object parameter)
        {
            try
            {

                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);

                // Verificar si el usuario ya tiene 50 comidas registradas en la fecha actual
                int dailyMealCount = _dbContext.Meals
                                               .Count(m => m.UserID == CurrentUser.LoggedInUserId && m.MealDate == todayDate);

                if (dailyMealCount >= 50)
                {
                    // Mostrar mensaje de error si se ha alcanzado el límite diario
                    StatusMessage = "Has alcanzado el límite máximo de 50 registros diarios.";
                    return; // Detener el proceso de guardado
                }

                var mealDate = DateOnly.FromDateTime(DateTime.Now);
                // Crear la nueva comida
                var meal = new Meal
                {
                    Name = MealName,
                    Kcal = MealKcal,
                    Weight = MealWeight,
                    Protein = MealProtein,
                    Carbohydrate = MealCarbohydrate,
                    Fat = MealFat,
                    MealDate = mealDate,
                    MealTypeID = SelectedMealTypeId,
                    UserID = CurrentUser.LoggedInUserId
                };

                // Guardar en la base de datos
                _dbContext.Meals.Add(meal);
                await _dbContext.SaveChangesAsync();

                // Actualizar mensaje de estado
                StatusMessage = "¡Comida guardada con éxito!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar la comida: {ex.Message}";
            }
        }

        // Validación del formulario
        private bool CanSaveMeal(object parameter)
        {
            return !string.IsNullOrEmpty(MealName) && SelectedMealTypeId > 0;
        }

        // Métodos anteriores...
        private void SetWelcomeMessage()
        {
            string currentUser = GetCurrentUser();
            Message = $"Welcome, {currentUser}!";
        }

        private string GetCurrentUser()
        {
            int userId = CurrentUser.LoggedInUserId;
            if (userId != 0)
            {
                var username = _dbContext.Users
                               .Where(u => u.ID == userId)
                               .Select(u => u.Username)
                               .FirstOrDefault();

                return username;
            }
            else
            {
                return "Guest";
            }
        }
    }
}
