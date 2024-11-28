using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Media3D;
using Microsoft.EntityFrameworkCore;
using MyHealthAI.Migrations;
using MyHealthAI.Models;
using MyHealthAI.Services;



namespace MyHealthAI.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext;
        private int? _height;
        private double? _weight;
        private string _name;
        private string _password;
        private string _passwordc;
        private string _email;
        private int? _age;
        private int _objectiveID;
        private int _genderID;
        private int _activityID;
        private int? _goalWeight;
        private string _statusMessage;
        private Window _registerWindow;
        private readonly IWindowService _windowService;

        public ObservableCollection<Activity> Activity { get; set; }
        public ObservableCollection<Objective> Objective { get; set; }
        public ObservableCollection<Gender> Gender { get; set; }

        // Constructor
        public RegisterViewModel(AppDbContext dbContext, IWindowService windowService, Window registerWindow)
        {

            _dbContext = dbContext;
            _windowService = windowService;
            _registerWindow = registerWindow;
            Activity = new ObservableCollection<Activity>(_dbContext.Activity.ToList()); 
            Activity.Insert(0, new Activity { ID = 0, Name = "Seleccione su actividad diaria" });
            Objective = new ObservableCollection<Objective>(_dbContext.Objectives.ToList());
            Objective.Insert(0, new Objective { ID = 0, Name = "Seleccione su Objetivo" });
            Gender = new ObservableCollection<Gender>(_dbContext.Gender.ToList());
            Gender.Insert(0, new Gender { ID = 0, Name = "Seleccione su genero" });

            SaveCommand = new RelayCommand(SaveUser);

        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Passwordc
        {
            get => _passwordc;
            set => SetProperty(ref _passwordc, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public int? Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public double? Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public int? Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public int ObjectiveID
        {
            get => _objectiveID;
            set => SetProperty(ref _objectiveID, value);
        }

        public int GenderID
        {
            get => _genderID;
            set => SetProperty(ref _genderID, value);
        }

        public int ActivityID
        {
            get => _activityID;
            set => SetProperty(ref _activityID, value);
        }

        public int? GoalWeight
        {
            get => _goalWeight;
            set => SetProperty(ref _goalWeight, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        // Comandos
        public ICommand SaveCommand { get; }
        public ICommand NavigateToLoginCommand => new RelayCommand(NavigateToLogin);

        // Métodos
        private async void SaveUser(object parameter)
        {
            try {

                RegisterAuth val = new RegisterAuth();
                (bool resultat, String missatge) = val.validate(Name, Password, Passwordc, Email, Height, Weight,ObjectiveID,ActivityID,GenderID);
                if (!resultat)
                {
                    MessageBox.Show(missatge);
                    return;
                }


                var newUser = new User
            {
               
                 Name = this.Name,
                 Password = this.Password,
                 Email = this.Email,
                 Age = this.Age,
                 ObjectiveID = this.ObjectiveID,
                 Height = this.Height,
                 Weight = this.Weight,
                 GenderID = this.GenderID,
                 ActivityID = this.ActivityID,
                 GoalWeight = this.GoalWeight
             };

                 _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();
                var dailyCalc = new DailyCalc(_dbContext);
                dailyCalc.CalculateDailyNeeds(newUser);
                MessageBox.Show("Usario Registrado con exito");
                _windowService.ShowWindow<LoginView>();
                _windowService.CloseWindow(_registerWindow);
 


            }
            catch(Exception ex) {

                MessageBox.Show("Error al registrar el usuaio");

            }


        }
        private void NavigateToLogin(Object parameter)
        {
            // Cierra la ventana actual y abre la de login
            _windowService.ShowWindow<LoginView>();
            Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
           
        }

        public void ShowLoginWindow()
        {
            // Abre la ventana de login
            _windowService.ShowWindow<LoginView>();  
        }

        public void CloseRegisterWindow(Window registerWindow)
        {
            // Cierra la ventana de registro
            _windowService.CloseWindow(registerWindow);
        }


    }
}
