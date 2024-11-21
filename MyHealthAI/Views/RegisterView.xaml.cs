﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using MyHealthAI.Models;
using MyHealthAI.Services;

namespace MyHealthAI
{
    public partial class RegisterView : Window
    {
        private int selectedOption = 10;

        public RegisterView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Password; 
            string email = txtEmail.Text;
            string passwordc = txtPasswordC.Password;

            int height = 0;
            if (int.TryParse(txtHeight.Text, out int h))
            {
                height = h;
            }

            double weight = 0;
            if (double.TryParse(txtWeight.Text, out double w))
            {
                weight = w;
            }

            int? goalWeight = null;
            if (int.TryParse(txtGoalWeight.Text, out int g))
            {
                goalWeight = g;
            }
            RegisterAuth val = new RegisterAuth();

            (bool resultat, String missatge) = val.validate(username, password,passwordc,email,height,weight);
          
            if (!resultat)
            {
                MessageBox.Show(missatge);
                return;

            }

            val.register(username,password,email,height,weight,selectedOption,goalWeight);
            MessageBox.Show("Successfully registered user");
            LoginView loginView= new LoginView();
            loginView.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (RadioButtonA.IsChecked == true)
            {
                selectedOption = 7;  
            }
            else if (RadioButtonB.IsChecked == true)
            {
                selectedOption = 8;  
            }
            else if (RadioButtonC.IsChecked == true)
            {
                selectedOption = 9;
            }
        }

       
    }
}
