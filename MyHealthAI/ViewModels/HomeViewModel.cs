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
    public class HomeViewModel : BaseViewModel, IDataErrorInfo
    {
        private readonly AppDbContext _dbContext;
        private string _mealName;
        private int? _mealKcal;
        private int? _mealWeight;
        private int? _mealProtein;
        private int? _mealCarbohydrate;
        private int? _mealFat;
        private int _selectedMealTypeId;
        private int? _dailyKcal;
        private int? _dailyProtein;
        private int? _dailyCarbohydrate;
        private List<ISeries> _series;
        private List<ISeries> _series1;
        private LabelVisual _title;
        private List<ISeries> _series2;
        private List<ISeries> _series3;
        private List<ISeries> _series4;
        private int? _dailyFat;
        private readonly CalorieService _calorieService;
        public string Message { get; private set; }
        private Axis[] _xAxes;
        private Axis[] _xAxes2;
        private Axis[] _yAxes;
        private Axis[] _yAxes1;
        private Axis[] _yAxes2;
        private Axis[] _yAxes3;
        private Axis[] _yAxes4;
        private int _totalCalories;
        private int _totalProtein;
        private int _totalCarbohydrate;
        private int _totalFat;
        private int _totalWater;
        private bool waterr = false;
        private bool oneTime = true;
        private readonly DailyCalc _dailyCalc;
        public string Error => null;
        public ObservableCollection<MealType> MealType { get; set; }
        private string _exerciseType;
        private double? _durationInMinutes;
        private double? _caloriesBurned;
        private List<string> _xAxisLabels;
        private List<double> _caloriesData;
        private List<double> _minutesData;
        private List<ISeries> _series5;
        private readonly ExerciseService _exerciseService;
        private string _statusMessage;

        public List<ISeries> Series5
        {
            get => _series5;
            set => SetProperty(ref _series5, value);
        }
        public List<string> XAxisLabels
        {
            get => _xAxisLabels;
            set => SetProperty(ref _xAxisLabels, value);
        }
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
        public LabelVisual Title
        {
            get => _title;
            set
            {
                _title = value;
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
        public Axis[] XAxes2
        {
            get => _xAxes2;
            set
            {
                _xAxes2 = value;
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
        public Axis[] YAxes1
        {
            get => _yAxes1;
            set
            {
                _yAxes1 = value;
                OnPropertyChanged();
            }
        }
        public Axis[] YAxes2
        {
            get => _yAxes2;
            set
            {
                _yAxes2 = value;
                OnPropertyChanged();
            }
        }
        public Axis[] YAxes3
        {
            get => _yAxes3;
            set
            {
                _yAxes3 = value;
                OnPropertyChanged();
            }
        }
        public Axis[] YAxes4
        {
            get => _yAxes4;
            set
            {
                _yAxes4 = value;
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
        public string ExerciseType
        {
            get => _exerciseType;
            set => SetProperty(ref _exerciseType, value);
        }
        public double? DurationInMinutes
        {
            get => _durationInMinutes;
            set => SetProperty(ref _durationInMinutes, value);
        }
        public double? CaloriesBurned
        {
            get => _caloriesBurned;
            set => SetProperty(ref _caloriesBurned, value);
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


        public ICommand DeleteExerciseCommand { get; }
        public ICommand SaveMealCommand { get; }
        public ICommand SaveExerciseCommand { get; }
        public ICommand DeleteMealCommand { get; }
        public ICommand WaterCommand { get; }
        public ICommand UpdateGrafic { get; }

        public HomeViewModel(AppDbContext dbContext, CalorieService calorieService, DailyCalc dailyCalc, ExerciseService exerciseService , NotificationService notificationService): base(notificationService)
        {
            _dailyCalc = dailyCalc;
            _dbContext = dbContext;
            _exerciseService = exerciseService;
            _calorieService = calorieService;
            MealType = new ObservableCollection<MealType>(_dbContext.MealType.ToList());
            MealType.Insert(0, new MealType { ID = 0, Name = "Seleccione el tipo de comida" });
            Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);
            Egrafic();

            // Inicializar el comando
            SaveMealCommand = new RelayCommand(SaveMeal, CanSaveMeal);
            SaveExerciseCommand = new RelayCommand(SaveExercise, CanSaveExercise);
            DeleteMealCommand = new RelayCommand(DeleteMeal);
            UpdateGrafic = new RelayCommand(Update);
            WaterCommand = new RelayCommand(AddWater);
            DeleteExerciseCommand = new RelayCommand(DeleteExercise);
      
        }

        public HomeViewModel(AppDbContext dbContext, CalorieService calorieService, NotificationService notificationService) : base(notificationService)
        {
            _dbContext = dbContext;
            _calorieService = calorieService;
        }

        private void Update(object parameter)
        {
            Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);
        }

        private async void DeleteExercise(object parameter)
        {
            try
            {

                var lastExercise = _dbContext.Exercises
                         .Where(m => m.UserID == CurrentUser.LoggedInUserId)
                         .OrderByDescending(m => m.ID)
                         .FirstOrDefault();


                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);

                var today = _dbContext.Exercises
                    .Where(m => m.UserID == CurrentUser.LoggedInUserId && m.Date == todayDate) // Filtra por fecha exacta
                    .Select(m => m.Date)
                    .FirstOrDefault();

                if (today == todayDate)
                {
                    // Eliminar la última comida
                    _dbContext.Exercises.Remove(lastExercise);

                    string deletedExerciseName = lastExercise.ExerciseType;

                    // Guardar cambios en la base de datos
                    await _dbContext.SaveChangesAsync();

                    // Actualizar mensaje de estado
                    Egrafic();
                    ShowSuccess($"El ejecicio '{deletedExerciseName}' ha sido eliminado con éxito.");
                }
                else
                {
                    ShowNotification("No hay ejecicios para eliminar hoy.");
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error detallado para depurar el problema
                ShowError($"Error al eliminar el ejercico {ex.Message}");
            }

        }

        private async void SaveExercise(object parameter)
        {
            try
            {

                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);

                // Verificar si el usuario ya tiene 50 comidas registradas en la fecha actual
                int dailyExCount = _dbContext.Exercises
                                               .Count(m => m.UserID == CurrentUser.LoggedInUserId && m.Date == todayDate);

                if (dailyExCount >= 15)
                {
                    // Mostrar mensaje de error si se ha alcanzado el límite diario
                    MessageBox.Show("Has alcanzado el límite máximo de 15 registros diarios.");
                    return; // Detener el proceso de guardado
                }


                double Dur = DurationInMinutes ?? 0;
                double Bur = CaloriesBurned ?? 0;

                var Date = DateOnly.FromDateTime(DateTime.Now);
                // Crear la nueva comida
                var Exercice = new Exercise
                {
                    ExerciseType = ExerciseType,
                    DurationInMinutes = Dur,
                    CaloriesBurned = Bur,
                    Date = Date,
                    UserID = CurrentUser.LoggedInUserId
                };

                // Guardar en la base de datos
                _dbContext.Exercises.Add(Exercice);
                await _dbContext.SaveChangesAsync();

    
                Egrafic();
                ShowSuccess("Ejercio registrado con exito");
            }
            catch (Exception ex)
            {
                ShowError($"Error al guardar el ejercicio: {ex.Message}");
            }


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


                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);

                var today = _dbContext.Meals
                    .Where(m => m.UserID == CurrentUser.LoggedInUserId && m.MealDate == todayDate) // Filtra por fecha exacta
                    .Select(m => m.MealDate)
                    .FirstOrDefault();

                if (today == todayDate)
                {
                    // Eliminar la última comida
                    _dbContext.Meals.Remove(lastMeal);

                    string deletedMealName = lastMeal.Name;

                    // Guardar cambios en la base de datos
                    await _dbContext.SaveChangesAsync();

                    // Actualizar mensaje de estado
                     ShowSuccess($"La comida '{deletedMealName}' ha sido eliminada con éxito.");
                    Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);
                }
                else
                {
                    ShowNotification("No hay comidas para eliminar hoy.");
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error detallado para depurar el problema
                ShowError($"Error al eliminar la comida: {ex.Message}");
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
            waterr = true;
            Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);

        }

        public async Task CalculateDailyNutrientIntake(int userID)
        {

            
                var (totalCalories, totalFat, totalProteins, totalCarbs,totalWater) =
                await _calorieService.GetDailyNutrientIntakeAsync(userID);
                var user = await _dbContext.Users
                .Where(u => u.ID == userID)
                .FirstOrDefaultAsync();

            _totalCalories = totalCalories;
            _totalFat = totalFat;
            _totalProtein = totalProteins;
            _totalCarbohydrate = totalCarbs;    
            _totalWater = totalWater;


             Grafic();
            

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
                    MessageBox.Show("Has alcanzado el límite máximo de 50 registros diarios.");
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

                string mealName = MealName;
                // Actualizar mensaje de estado
                ShowSuccess($"¡La comida '{mealName}' ha sido guardada con éxito!");
                Task task = CalculateDailyNutrientIntake(CurrentUser.LoggedInUserId);

            }
            catch (Exception ex)
            {
                ShowError($"Error al guardar la comida: {ex.Message}");
            }
        }

        private bool CanSaveMeal(object parameter)
        {
            return string.IsNullOrEmpty(this[nameof(MealName)]) &&
                   string.IsNullOrEmpty(this[nameof(MealKcal)]) &&
                   string.IsNullOrEmpty(this[nameof(MealWeight)]) &&
                   string.IsNullOrEmpty(this[nameof(MealProtein)]) &&
                   string.IsNullOrEmpty(this[nameof(MealCarbohydrate)]) &&
                   string.IsNullOrEmpty(this[nameof(MealFat)]) &&
                   SelectedMealTypeId > 0 &&
                   !string.IsNullOrEmpty(MealName) &&
                   MealKcal.HasValue && MealKcal > 0 &&
                   MealWeight.HasValue && MealWeight > 0 &&
                   MealProtein.HasValue && MealProtein > 0 &&
                   MealCarbohydrate.HasValue && MealCarbohydrate > 0 &&
                   MealFat.HasValue && MealFat > 0;

        }

        private bool CanSaveExercise(object parameter)
        {
            return string.IsNullOrEmpty(this[nameof(ExerciseType)]) &&
                 string.IsNullOrEmpty(this[nameof(DurationInMinutes)]) &&
                   string.IsNullOrEmpty(this[nameof(CaloriesBurned)]) && !string.IsNullOrEmpty(ExerciseType) &&
                   ExerciseType.Length != 0 &&
                   DurationInMinutes > 0 ||
                   CaloriesBurned > 0;
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
                    case nameof(ExerciseType):
                        if (!string.IsNullOrEmpty(ExerciseType) && ExerciseType.Length > 15)
                            return "El nombre de la comida no debe exceder los 15 caracteres.";
                        break;
                    case nameof(DurationInMinutes):
                        if (DurationInMinutes.HasValue && (DurationInMinutes < 0 || DurationInMinutes > 10000))
                            return "Los minutos deben estar entre 0 y 10,000.";
                        break;
                    case nameof(CaloriesBurned):
                        if (CaloriesBurned.HasValue && (CaloriesBurned < 0 || CaloriesBurned > 10000))
                            return "Las calorias deben estar entre 0 y 10,000.";
                        break;
                }
                return null;
            }
        }

        public async void Grafic()
        {

            var user = await _dbContext.Users
            .Where(u => u.ID == CurrentUser.LoggedInUserId)
            .FirstOrDefaultAsync();

            if (!waterr)
            {

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
                 Fill = new SolidColorPaint(new SKColor(255, 165, 0)),
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
                Fill = new SolidColorPaint(new SKColor(255, 0, 0)),
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
                Fill = new SolidColorPaint(new SKColor(255, 223, 0)),
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
                Fill = new SolidColorPaint(new SKColor(128, 128, 0)),
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
                YAxes1 = new Axis[]
                {
                new Axis
                    {
                     MinLimit = 0,
                     MaxLimit = user.DailyPro,
                     Labeler = value => value.ToString("0"),
                     IsVisible = true
                    }
                    };

                YAxes2 = new Axis[]
                {
                new Axis
                    {
                     MinLimit = 0,
                     MaxLimit = user.DailyCar,
                     Labeler = value => value.ToString("0"),
                     IsVisible = true
                    }
                    };

                YAxes3 = new Axis[]
                 {
                new Axis
                    {
                     MinLimit = 0,
                     MaxLimit = user.DailyFat,
                     Labeler = value => value.ToString("0"),
                     IsVisible = true
                    }
                    };
                waterr = false;
            }
            if (waterr || oneTime)
            {
                oneTime = false;
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
                Fill = new SolidColorPaint(new SKColor(0, 191, 255)),
                IgnoresBarPosition = true
                }

            };

                YAxes4 = new Axis[]
    {
                new Axis
                    {
                     MinLimit = 0,
                     MaxLimit = user.DailyWater,
                     Labeler = value => value.ToString("0"),
                     IsVisible = true
                    }
                };
                waterr = false;
            }
        }

        private async void Egrafic()
        {





            var userId = CurrentUser.LoggedInUserId;
            var exerciseData = await _exerciseService.GetExerciseDataForWeekAsync(userId);

            // Asignar las etiquetas para el eje X (días de la semana)
            XAxisLabels = exerciseData.Select(d => d.Date.DayOfWeek.ToString()).ToList();

            // Asignar los datos de calorías quemadas y minutos entrenados
            _caloriesData = exerciseData.Select(d => d.TotalCaloriesBurned).ToList();
            _minutesData = exerciseData.Select(d => d.TotalMinutesTrained).ToList();



            // Crear las series para el gráfico (líneas)
            Series5 = new List<ISeries>
            {
                new LineSeries<double>
                {
                    Values = _caloriesData,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.Red), // Color para la línea de calorías
                    LineSmoothness = 0, // Para una línea más recta, si quieres más suavidad, puedes cambiar el valor
                    IsHoverable = true, // Habilitar la interacción con el ratón
                    Name = "Calorias",
                    XToolTipLabelFormatter =
                        (chartPoint) => $"{chartPoint.Context.Series.Name}:"
                },
                new LineSeries<double>
                {
                    Values = _minutesData,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.Green), // Color para la línea de minutos
                    LineSmoothness = 0, // Para una línea más recta
                    IsHoverable = true, // Habilitar la interacción con el ratón
                    Name = "Ejercicio",
                     XToolTipLabelFormatter =
                        (chartPoint) => $"{chartPoint.Context.Series.Name}:"
                }
            };


            XAxes2 = new Axis[]
{
                new Axis
                    {
                    // Establecer las etiquetas para el eje X
                    Labels = new[] { "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do" },
                    IsVisible = true, // Hacer que el eje X sea visible
                    LabelsRotation = 0, // Ángulo de rotación de las etiquetas
                    }
                };



        }

    }
    
}
