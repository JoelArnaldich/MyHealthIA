using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
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
        private List<ISeries> _series;
        private readonly AppDbContext _dbContext;
        private int _userId;
        public Axis[] XAxes
        {
            get => _xAxes;
            set
            {
                _xAxes = value;
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
        public StatsViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Ggrafic();
        }
        public async void Ggrafic()
        {
            _userId = CurrentUser.LoggedInUserId; 
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
            }
          
        }
    }

