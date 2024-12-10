using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DotnetGeminiSDK.Client;
using MyHealthAI.Services;

namespace MyHealthAI
{
    public partial class AiPage : Page
    {
        public AiPage()
        {
            InitializeComponent();
            this.DataContext = new AiViewModel(new Services.GeminiClient()); // Asume que tienes un cliente GeminiClient configurado
        }
    }
}
