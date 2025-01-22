using System.Windows;
using MyHealthAI.Models;
using MyHealthAI.ViewModels;

namespace MyHealthAI
{

    public partial class RegisterView : Window
    {

        bool login1 = false;

        public RegisterView()
        {
            InitializeComponent();
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            var dbContext = new AppDbContext();
            this.DataContext = new RegisterViewModel(dbContext ,notificationService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is RegisterViewModel viewModel)
            {
                viewModel.Password = PasswordBoxControl.Password;
            }
        }
        private void PasswordBox_PasswordChanged2(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is RegisterViewModel viewModel)
            {
                viewModel.Passwordc = PasswordBoxControl.Password;
            }
        }
        private void ChLogin_Change(object sender, EventArgs e)
        {
            login1 = true;
            LoginView login = new LoginView();
            this.Close();
            login.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!login1)
            {
                base.OnClosed(e);
                Application.Current.Shutdown();
            }
        }
    }
}
