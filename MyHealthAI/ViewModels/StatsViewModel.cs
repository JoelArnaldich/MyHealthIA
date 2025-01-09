using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealthAI.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private Axis[] _xAxes;
        private Axis[] _xAxes2;
        private Axis[] _yAxes;
        private Axis[] _yAxes1;
        private Axis[] _yAxes2;
        private Axis[] _yAxes3;
        private Axis[] _yAxes4;
        private readonly AppDbContext _dbContext;

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
        public StatsViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Grafic();
            

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



    }
}
