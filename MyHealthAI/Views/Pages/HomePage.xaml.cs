using System.Windows;
using System.Windows.Controls;
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
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            var dbContext = new AppDbContext();
            var exerciseService = new ExerciseService(dbContext);
            var DailyCalc = new DailyCalc(dbContext);
            var calorieService = new CalorieService(new AppDbContext());
            this.DataContext = new HomeViewModel(dbContext,calorieService,DailyCalc,exerciseService,notificationService);
        }

        private void OpenPopup_Click(object sender, RoutedEventArgs e)
        {
            var popup = new MealsPopUp();
            var ownerWindow = Window.GetWindow(this);
            popup.ShowDialog();
        }




    }

}
