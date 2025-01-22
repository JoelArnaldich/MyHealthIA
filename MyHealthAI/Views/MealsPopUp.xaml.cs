using MyHealthAI.Models;
using MyHealthAI.Services;
using MyHealthAI.ViewModels;
using System.Windows;

namespace MyHealthAI
{
    public partial class MealsPopUp : Window
    {
        public MealsPopUp()
        {
            var dbContext = new AppDbContext();
            var calorieService = new CalorieService(new AppDbContext());
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            this.DataContext = new MealsPopUpViewModel(dbContext, calorieService,notificationService);
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
