using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DotnetGeminiSDK.Client;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Models;
using MyHealthAI.Services;

namespace MyHealthAI
{
    public partial class AiPage : Page
    {
        public AiPage()
        {
            var notificationManager = new Notifications.Wpf.NotificationManager();
            var notificationService = new Services.NotificationService(notificationManager);
            InitializeComponent();
            var dbcontext = new AppDbContext();
            var userDataService = new UserDataService(dbcontext);
            this.DataContext = new AiViewModel(new Services.GeminiClient(),dbcontext,notificationService);
        }
       

    }

}



