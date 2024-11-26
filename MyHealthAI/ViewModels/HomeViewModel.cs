using System;
using System.Linq;
using System.Windows.Input;
using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyHealthAI.Services;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Documents;

namespace MyHealthAI.ViewModels
{
    public class HomeViewModel : BaseViewModel, IDataErrorInfo
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
        private string _message2;
        private int? _dailyKcal;
        private int? _dailyProtein;
        private int? _dailyCarbohydrate;
        private int? _dailyFat;
        private readonly CalorieService _calorieService;
        public string Message { get; private set; }


        // ObservableCollection para enlazar con ComboBox
        public ObservableCollection<MealType> MealType { get; set; }



        // Propiedades para enlazar con la Vista (Formulario)
        public int? DailyKcal
        {
            get => _dailyKcal;
            set => SetProperty(ref _dailyKcal, value);
        }

        public int? DailyProtein
        {
            get => _dailyProtein;
            set => SetProperty(ref _dailyProtein, value);
        }

        public int? DailyCarbohydrate
        {
            get => _dailyCarbohydrate;
            set => SetProperty(ref _dailyCarbohydrate, value);
        }

        public int? DailyFat
        {
            get => _dailyFat;
            set => SetProperty(ref _dailyFat, value);
        }

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
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }







        // Comando para guardar la comida
        public ICommand SaveMealCommand { get; }
        public ICommand DeleteMealCommand { get; }


        public HomeViewModel(AppDbContext dbContext, CalorieService calorieService)
        {
            _dbContext = dbContext;
            _calorieService = calorieService;
            MealType = new ObservableCollection<MealType>(_dbContext.MealType.ToList()); // Cargar MealTypes desde la base de datos
            MealType.Insert(0, new MealType { ID = 0, Name = "Seleccione el tipo de comida" });
            SetWelcomeMessage();
            Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);


            // Inicializar el comando
            SaveMealCommand = new RelayCommand(SaveMeal, CanSaveMeal);
            DeleteMealCommand = new RelayCommand(DeleteMeal);
      
        }

        private async void DeleteMeal(object parameter)
        {
            try
            {
                // Encontrar la última comida registrada por el usuario actual
                var lastMeal = _dbContext.Meals
                                         .Where(m => m.UserID == CurrentUser.LoggedInUserId)
                                         .OrderByDescending(m => m.ID)
                                         .FirstOrDefault();

                if (lastMeal != null)
                {
                    // Eliminar la última comida
                    _dbContext.Meals.Remove(lastMeal);

                    string deletedMealName = lastMeal.Name;

                    // Guardar cambios en la base de datos
                    await _dbContext.SaveChangesAsync();

                    // Actualizar mensaje de estado
                    StatusMessage = $"La comida '{deletedMealName}' ha sido eliminada con éxito.";
                }
                else
                {
                    StatusMessage = "No hay comidas para eliminar.";
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error detallado para depurar el problema
                StatusMessage = $"Error al eliminar la comida: {ex.Message}";
            }
        }


        public async Task CalculateDailyNutrientIntake(int userID)
        {
            // Llamada a GetDailyNutrientIntakeAsync que devuelve tuplas con valores posiblemente nulos


            
                var (totalCalories, totalFat, totalProteins, totalCarbs) =
                await _calorieService.GetDailyNutrientIntakeAsync(userID);
                var user = await _dbContext.Users
                .Where(u => u.ID == userID)
                .FirstOrDefaultAsync();

    

            SetMessage($"Te Faltan {totalCalories} de {user.DailyKcal}, {totalFat}g de grasa, {totalProteins}g de proteínas, {totalCarbs}g de carbohidratos");
 
            
         }


        public void SetMessage(string message)
        {
            Message = message;
            OnPropertyChanged(nameof(Message));  // Esto asegura que la vista se actualice
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

                int Protein = MealProtein ?? 0;
                int Fat = MealFat ?? 0;
                int Carbohydrate = MealCarbohydrate ?? 0;
                int Kcal = MealKcal ?? 0;


                var mealDate = DateOnly.FromDateTime(DateTime.Now);
                // Crear la nueva comida
                var meal = new Meal
                {
                    Name = MealName,
                    Kcal = Kcal,
                    Weight = MealWeight,
                    Protein = Protein,
                    Carbohydrate = Carbohydrate,
                    Fat = Fat,
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
            return string.IsNullOrEmpty(this[nameof(MealName)]) &&
                   string.IsNullOrEmpty(this[nameof(MealKcal)]) &&
                   string.IsNullOrEmpty(this[nameof(MealWeight)]) &&
                   string.IsNullOrEmpty(this[nameof(MealProtein)]) &&
                   string.IsNullOrEmpty(this[nameof(MealCarbohydrate)]) &&
                   string.IsNullOrEmpty(this[nameof(MealFat)]) &&
                   SelectedMealTypeId > 0;
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
                               .Select(u => u.Name)
                               .FirstOrDefault();

                return username;
            }
            else
            {
                return "Guest";
            }
        }

         public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(MealKcal):
                        if (MealKcal.HasValue && (MealKcal < 0 || MealKcal > 10000))
                            return "Calorías deben estar entre 0 y 10,000.";
                        break;

                    case nameof(MealName):
                        if (!string.IsNullOrEmpty(MealName) && MealName.Length > 15)
                        {
                            return "El nombre de la comida no debe exceder los 15 caracteres.";
                        }
                        break;


                    case nameof(MealWeight):
                        if (MealWeight.HasValue && (MealWeight < 0 || MealWeight > 10000))
                            return "Peso debe estar entre 0 y 10,000 gramos.";
                        break;

                    case nameof(MealProtein):
                        if (MealProtein.HasValue && (MealProtein < 0 || MealProtein > 10000))
                            return "Proteínas deben estar entre 0 y 10,000.";
                        break;

                    case nameof(MealCarbohydrate):
                        if (MealCarbohydrate.HasValue && (MealCarbohydrate < 0 || MealCarbohydrate > 10000))
                            return "Carbohidratos deben estar entre 0 y 10,000.";
                        break;

                    case nameof(MealFat):
                        if (MealFat.HasValue && (MealFat < 0 || MealFat > 10000))
                            return "Grasas deben estar entre 0 y 10,000.";
                        break;
                }
                return null;
            }
        }

        public string Error => null;

    
    }
}
