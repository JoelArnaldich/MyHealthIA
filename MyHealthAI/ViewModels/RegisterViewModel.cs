﻿
using System.Collections.ObjectModel;
using System.Windows.Input;
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
        private double? _goalWeight;
        private string _statusMessage;

        public ObservableCollection<Activity> Activity { get; set; }
        public ObservableCollection<Objective> Objective { get; set; }
        public ObservableCollection<Gender> Gender { get; set; }

        // Constructor
        public RegisterViewModel(AppDbContext dbContext, NotificationService notificationService): base(notificationService)
        {

            _dbContext = dbContext;
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

        public double? GoalWeight
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


        public ICommand SaveCommand { get; }
 

        // Métodos
        private async void SaveUser(object parameter)
        {
            try {

                RegisterAuth val = new RegisterAuth();
                (bool resultat, String missatge) = val.validate(Name, Password, Passwordc, Email, Height, Weight,ObjectiveID,ActivityID,GenderID,GoalWeight,Age);
                if (!resultat)
                {
                    ShowError(missatge);
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
                ShowSuccess("Usario Registrado con exito");


            }
            catch(Exception ex) {

            }


        }

    }
}
