using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
using MyHealthAI.Services;
using MyHealthAI.ViewModels;

namespace MyHealthAI
{

    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            var dbContext = new AppDbContext();
            var exerciseService = new ExerciseService(dbContext);
            var DailyCalc = new DailyCalc(dbContext);
            var calorieService = new CalorieService(new AppDbContext());
            this.DataContext = new HomeViewModel(dbContext,calorieService,DailyCalc,exerciseService);
        }

    }
}
