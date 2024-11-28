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
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

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
        private List<ISeries> _series;
        private List<ISeries> _series1;
        private List<ISeries> _series2;
        private List<ISeries> _series3;
        private List<ISeries> _series4;
        private int? _dailyFat;
        private readonly CalorieService _calorieService;
        public string Message { get; private set; }
        private Axis[] _xAxes;
        private Axis[] _yAxes;
        private int _totalCalories;
        private int _totalProtein;
        private int _totalCarbohydrate;
        private int _totalFat;
        private int _totalWater;





        public ObservableCollection<MealType> MealType { get; set; }

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
        public List<ISeries> Series4
        {
            get => _series4;
            set
            {
                _series4 = value;
                OnPropertyChanged(); // Notificar cambios al UI
            }
        }

        public Axis[] XAxes
        {
            get => _xAxes;
            set
            {
                _xAxes = value;
                OnPropertyChanged();
            }
        }

        public Axis[] YAxes
        {
            get => _yAxes;
            set
            {
                _yAxes = value;
                OnPropertyChanged();
            }
        }


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



        public ICommand SaveMealCommand { get; }
        public ICommand DeleteMealCommand { get; }
        public ICommand WaterCommand { get; }


        public HomeViewModel(AppDbContext dbContext, CalorieService calorieService)
        {
            _dbContext = dbContext;
            _calorieService = calorieService;
            MealType = new ObservableCollection<MealType>(_dbContext.MealType.ToList());
            MealType.Insert(0, new MealType { ID = 0, Name = "Seleccione el tipo de comida" });
            Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);
            SetWelcomeMessage();

            // Inicializar el comando
            SaveMealCommand = new RelayCommand(SaveMeal, CanSaveMeal);
            DeleteMealCommand = new RelayCommand(DeleteMeal);
            WaterCommand = new RelayCommand(AddWater);
      
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
                    Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);
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

        public bool HasWaterEntryForToday(int userId)
        {
            // Obtener la fecha de hoy
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Verificar si ya existe un registro en la tabla DialyWater para el usuario y la fecha de hoy
            var entryExists = _dbContext.Water
                                        .Any(w => w.UserID == userId && w.Date == today);

            return entryExists;
        }


        private async void AddWater(object parameter)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Buscar si ya existe un registro para el usuario y la fecha de hoy
            var waterEntry = await _dbContext.Water
                                             .FirstOrDefaultAsync(w => w.UserID == CurrentUser.LoggedInUserId && w.Date == today);

            if (waterEntry != null)
            {
                // Si ya existe un registro, sumamos 250ml al valor actual
                waterEntry.WaterMl += 250; // Suponiendo que 'Amount' es el campo que guarda la cantidad de agua

                _dbContext.Water.Update(waterEntry);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Si no existe un registro para hoy, creamos uno nuevo
                var newWaterEntry = new Water
                {
                    UserID = CurrentUser.LoggedInUserId,
                    Date = today,
                    WaterMl = 250 // Iniciar con 250ml
                };

                _dbContext.Water.Add(newWaterEntry);
                await _dbContext.SaveChangesAsync();
       
            }

            Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);

        }


        public async Task CalculateDailyNutrientIntake(int userID)
        {



            
                var (totalCalories, totalFat, totalProteins, totalCarbs,totalWater) =
                await _calorieService.GetDailyNutrientIntakeAsync(userID);
                var user = await _dbContext.Users
                .Where(u => u.ID == userID)
                .FirstOrDefaultAsync();
            string currentUser = GetCurrentUser();

            _totalCalories = totalCalories;
            _totalFat = totalFat;
            _totalProtein = totalProteins;
            _totalCarbohydrate = totalCarbs;
            _totalWater = totalWater;

            Grafic();

        }


        public void SetMessage(string message)
        {
            Message = message;
            OnPropertyChanged(nameof(Message));  
        }


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
                Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar la comida: {ex.Message}";
            }
        }

        private void SetWelcomeMessage()
        {
            string currentUser = GetCurrentUser();
            Message = $"Welcome, {currentUser}!";
        }


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



        private async void Grafic()
        {

            var user = await _dbContext.Users
            .Where(u => u.ID == CurrentUser.LoggedInUserId)
            .FirstOrDefaultAsync();



            Series = new List<ISeries>

            {
                new ColumnSeries<int?>
                {
                IsHoverable = false, // Desactiva tooltips en la serie
                Values = new int?[] { user.DailyKcal},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(30, 30, 30, 30)),
                IgnoresBarPosition = true
                },
            new ColumnSeries<int>
            {
                Values = new int[] { _totalCalories},
                Stroke = null,
                 Fill = new SolidColorPaint(new SKColor(100, 149, 237)),
                IgnoresBarPosition = true
                }

            };



            Series1 = new List<ISeries>

            {
                new ColumnSeries<int?>
                {
                IsHoverable = false, // Desactiva tooltips en la serie
                Values = new int?[] { user.DailyPro},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(30, 30, 30, 30)),
                IgnoresBarPosition = true
                },
            new ColumnSeries<int>
            {
                Values = new int[] { _totalProtein},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(100, 149, 237)),
                IgnoresBarPosition = true
                }

            };



            Series2 = new List<ISeries>

            {
                new ColumnSeries<int?>
                {
                IsHoverable = false, // Desactiva tooltips en la serie
                Values = new int?[] { user.DailyCar},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(30, 30, 30, 30)),
                IgnoresBarPosition = true
                },
            new ColumnSeries<int>
            {
                Values = new int[] { _totalCarbohydrate},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(100, 149, 237)),
                IgnoresBarPosition = true
                }

            };




            Series3 = new List<ISeries>

            {
                new ColumnSeries<int?>
                {
                IsHoverable = false, // Desactiva tooltips en la serie
                Values = new int?[] { user.DailyFat},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(30, 30, 30, 30)),
                IgnoresBarPosition = true
                },
            new ColumnSeries<int>
            {
                Values = new int[] { _totalFat},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(100, 149, 237)),
                IgnoresBarPosition = true
                }

            };


            Series4 = new List<ISeries>

            {
                new ColumnSeries<int?>
                {
                IsHoverable = false, // Desactiva tooltips en la serie
                Values = new int?[] {user.DailyWater},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(30, 30, 30, 30)),
                IgnoresBarPosition = true
                },
            new ColumnSeries<int>
            {
                Values = new int[] { _totalWater},
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(100, 149, 237)),
                IgnoresBarPosition = true
                }

            };
            XAxes = new Axis[]
            {
                new Axis
                {
                    IsVisible = false 
                }
            };

            YAxes = new Axis[] 
                {
                     new Axis
                    {
                        MinLimit = 0,         
                        MaxLimit = user.DailyKcal,        
                        Labeler = value => value.ToString("0"),
                        IsVisible = true       
                    }
                };
              }

    
    }
}
