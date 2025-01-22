using MyHealthAI.Models;
using MyHealthAI.ViewModels;
using System.Windows.Controls;


namespace MyHealthAI
{

    public partial class StatsPage : Page
    {
        public StatsPage()
        {
            InitializeComponent();
            var dbContext = new AppDbContext();
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            this.DataContext = new StatsViewModel(dbContext,notificationService);
        }
    }
}
