using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
using MyHealthAI.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHealthAI.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private Axis[] _xAxes;
        private Axis[] _xAxes2;
        private Axis[] _yAxes;
        private List<ISeries> _series;
        private List<ISeries> _series1;
        private List<ISeries> _seriesCalories;  
        private readonly AppDbContext _dbContext;
        private int _userId;
        private readonly WaterService _waterService;
        private readonly MealService _mealService;
        private readonly ExerciseService _exerciseService;
        private List<int> _waterData;

        public Axis[] YAxes
        {
            get => _yAxes;
            set
            {
                _yAxes = value;
                OnPropertyChanged();
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
        public List<ISeries> Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(); 
            }
        }
        public List<ISeries> Series1
        {
            get => _series1;
            set
            {
                _series1 = value;
                OnPropertyChanged();
            }
        }
        public List<ISeries> SeriesCalories
        {
            get => _seriesCalories;
            set
            {
                _seriesCalories = value;
                OnPropertyChanged();
            }
        }



        public StatsViewModel(AppDbContext dbContext, NotificationService notificationService) : base(notificationService)
        {
            _userId = CurrentUser.LoggedInUserId;
            _waterService = new WaterService(dbContext);
            _exerciseService = new ExerciseService(dbContext);
            _mealService = new MealService(dbContext);
            _dbContext = dbContext;
            Task.Run(async () =>
            {
                await Ggrafic();
                await Wgrafic();
                await Cgrafic();
            });
        }
        public async Task Ggrafic()
        {

                    var user = await _dbContext.Users
           .Where(u => u.ID == _userId)
           .FirstOrDefaultAsync();

            double goalWeight = user.GoalWeight.Value;

            var weightHistory = await _dbContext.Weights
                .Where(w => w.UserID == _userId)
                .OrderBy(w => w.Date)
                .ToListAsync();

            var goalWeightList = weightHistory.Select(w => goalWeight).ToList();
            goalWeightList.Add(goalWeight);

            var weights = weightHistory.Select(w => w.Weight).ToList();

            var dates = weightHistory.Select(w => w.Date.ToString("dd/MM/yyyy")).ToList();

            double currentWeight = user.Weight ?? 0.0;
            weights.Add(currentWeight);
            dates.Add(DateTime.Now.ToString("dd/MM/yyyy"));

            Series = new List<ISeries>
            {
                new LineSeries<double>
                {
                    Values = goalWeightList,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.Red),
                    LineSmoothness = 0,
                    IsHoverable = true,
                    Name = "Peso Objetivo",
                    XToolTipLabelFormatter =
                        (chartPoint) => $"{chartPoint.Context.Series.Name}: "
                },
                new LineSeries<double>
                {
                    Values = weights,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.Green),
                    LineSmoothness = 0,
                    IsHoverable = true,
                    Name = "Peso Actual",
                    XToolTipLabelFormatter =
                        (chartPoint) => $"{chartPoint.Context.Series.Name}: "
                }
            };

                        XAxes = new Axis[]
                        {
                new Axis
                {
                    Labels = dates,
                    IsVisible = true
                }
                        };

                        YAxes = new Axis[]
                        {
                new Axis
                {
                    MinLimit = weights.Min() - 5, // Ajustar el mínimo para mostrar mejor la diferencia
                    MaxLimit = weights.Max() + 5, // Ajustar el máximo para mostrar la diferencia
                    Labeler = value => value.ToString("F1"), // Mostrar con un decimal
                    IsVisible = true
                }
                        };

                    }

        public async Task Wgrafic()
        {
            // Obtener los datos del usuario
            var user = await _dbContext.Users
                .Where(u => u.ID == _userId)
                .FirstOrDefaultAsync();


            var waterData = await _waterService.GetWaterDataForWeekAsync(_userId);
            var dailyGoals = Enumerable.Repeat(user.DailyWater, 7).ToList();
            var consumedWater = waterData.Select(d => d.TotalWaterMl).ToList();

            Series1 = new List<ISeries>
            {
                new ColumnSeries<int?>
                {
                    IsHoverable = false, // Desactiva tooltips en la serie
                    Values = dailyGoals, // Valores del objetivo diario
                    Stroke = null,
                    Fill = new SolidColorPaint(new SKColor(192, 192, 192, 128)), // Color gris semitransparente
                    IgnoresBarPosition = true // Permite la superposición
                },
                new ColumnSeries<int>
                {
                    Values = consumedWater, // Valores del agua consumida
                    Stroke = null,
                    Fill = new SolidColorPaint(new SKColor(0, 191, 255)), // Color azul
                    IgnoresBarPosition = true // Permite la superposición
                }
            };


            // Configurar los ejes X con las etiquetas de los días de la semana
            XAxes2 = new Axis[]
            {
        new Axis
        {
            Labels = new[] { "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do" }, // Días de la semana
            IsVisible = true,
            LabelsRotation = 0 // Rotación de etiquetas
        }
            };

            // Notificar cambios en las propiedades enlazadas al gráfico
            OnPropertyChanged(nameof(Series1));
            OnPropertyChanged(nameof(XAxes2));
        }

        public async Task Cgrafic()
        {
            // Obtener las calorías quemadas y consumidas
            var exerciseData = await _exerciseService.GetExerciseDataForWeekAsync(_userId);
            var mealData = await _mealService.GetMealDataForWeekAsync(_userId);

            // Listas para las calorías quemadas y consumidas
            var burnedCalories = exerciseData.Select(d => (int)d.TotalCaloriesBurned).ToList();
            var consumedCalories = mealData.Select(d => d.Kcal).ToList();

            // Días de la semana
            var weekDays = new[] { "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do" };

            SeriesCalories = new List<ISeries>
            {
                new LineSeries<int>
                {
                    Values = burnedCalories,
                    Fill = null,
                    Stroke = new SolidColorPaint(new SKColor(192, 192, 192)), // Gris
                    LineSmoothness = 0,
                    IsHoverable = true,
                    Name = "Calorías Quemadas",
                    XToolTipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}:"
                },
                new LineSeries<int>
                {
                    Values = consumedCalories,
                    Fill = null,
                    Stroke = new SolidColorPaint(new SKColor(30, 144, 255)), // Azul
                    LineSmoothness = 0,
                    IsHoverable = true,
                    Name = "Calorías Consumidas",
                    XToolTipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name}:"
                }
            };

            // Configuración de los ejes X
            XAxes2 = new Axis[]
            {
                new Axis
                {
                    Labels = weekDays,
                    IsVisible = true
                }
            };

            // Notificar que se han cambiado los valores del gráfico de calorías
            OnPropertyChanged(nameof(SeriesCalories));
            OnPropertyChanged(nameof(XAxes));
        }
    }
}

