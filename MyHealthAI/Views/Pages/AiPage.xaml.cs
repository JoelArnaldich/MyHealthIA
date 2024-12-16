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
            InitializeComponent();
            var dbcontext = new AppDbContext();
            var userDataService = new UserDataService(dbcontext);
            this.DataContext = new AiViewModel(new Services.GeminiClient(),dbcontext);
        }
       
        
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
  
                e.Handled = true;

                // Ejecutar el comando de enviar mensaje
                var command = ((AiViewModel)this.DataContext).SendMessageCommand;
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }
        }

    }

}



