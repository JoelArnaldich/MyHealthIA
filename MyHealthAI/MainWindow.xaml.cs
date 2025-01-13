using MyHealthAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyHealthAI
{
    public partial class MainWindow : Window
    {
        public MainWindow() {

           InitializeComponent();
           navframe.Navigate(new HomePage());


        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.Navlink);

        }

        private void LogOut(object sender, EventArgs e)
        {

            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }


    }
}
