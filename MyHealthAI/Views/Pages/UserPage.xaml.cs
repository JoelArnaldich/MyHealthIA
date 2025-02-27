﻿using MyHealthAI.Models;
using MyHealthAI.Services;
using MyHealthAI.ViewModels;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyHealthAI
{

    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            var dbContext = new AppDbContext();
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            var DailyCalc = new DailyCalc(dbContext);
            this.DataContext = new UserViewModel(dbContext,notificationService,DailyCalc);
        }
    }
}
