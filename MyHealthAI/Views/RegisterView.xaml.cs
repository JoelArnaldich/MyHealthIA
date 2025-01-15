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

    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            var dbContext = new AppDbContext();
            this.DataContext = new RegisterViewModel(dbContext ,this,notificationService);
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
            LoginView login = new LoginView();
            this.Close();
            login.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
