using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MyHealthAI;
using MyHealthAI.Models;
using MyHealthAI.Services;
using MyHealthAI.ViewModels;

public class UserViewModel : BaseViewModel
{
    private readonly AppDbContext _dbContext;

    // Propiedades relacionadas con el usuario
    private double _newWeight;
    private int _newHeight;
    private double _newGoal;
    private int _newAge;
    private int _newObjective;



    public double NewWeight
    {
        get => _newWeight;
        set
        {
            _newWeight = value;
            OnPropertyChanged(nameof(NewWeight));
        }
    }
    public int NewHeight
    {
        get => _newHeight;
        set
        {
            _newHeight = value;
            OnPropertyChanged(nameof(NewHeight));
        }
    }
    public double NewGoal
    {
        get => _newGoal;
        set
        {
            _newGoal = value;
            OnPropertyChanged(nameof(NewGoal));
        }
    }
    public int NewAge
    {
        get => _newAge;
        set
        {
            _newAge = value;
            OnPropertyChanged(nameof(NewAge));
        }
    }
    public int NewObjective
    {
        get => _newObjective;
        set => SetProperty(ref _newObjective, value);
    }

    public ObservableCollection<Objective> Objective { get; set; }

    // Comandos
    public ICommand UpdateUserDetailsCommand { get; }
    public ICommand DeleteAllDataCommand { get; }

    // Constructor
    public UserViewModel(AppDbContext appDbContext, NotificationService notificationService) : base(notificationService)
    {
        _dbContext = appDbContext;

        Objective = new ObservableCollection<Objective>(_dbContext.Objectives.ToList());
        var user = _dbContext.Users.Find(CurrentUser.LoggedInUserId);

        if (user != null)
        {
            NewWeight = user.Weight ?? 0;
            NewHeight = (int)user.Height;
            NewGoal = (double)user.GoalWeight;
            NewAge = (int)user.Age;
            NewObjective = user.ObjectiveID;

        }
        else
        {
            MessageBox.Show("Error: Usuario no encontrado.");
        }

        UpdateUserDetailsCommand = new RelayCommand(_ => UpdateUserDetails());
        DeleteAllDataCommand = new RelayCommand(_ => DeleteAllData());

    }

    // Método para actualizar los detalles del usuario
    private void UpdateUserDetails()
    {
        try
        {
            if (NewWeight < 10 || NewWeight > 1000)
            {
                ShowError("El peso debe estar entre 10 y 1000 kg.");
                return;
            }

            if (NewHeight < 10 || NewHeight > 400)
            {
                ShowError("La altura debe estar entre 10 y 400 cm.");
                return;
            }

            if (NewAge < 10 || NewAge > 200)
            {
                ShowError("La edad debe estar entre 10 y 200 años.");
                return;
            }

            if (NewGoal < 10 || NewGoal > 1000)
            {
                ShowError("El peso objetivo debe estar entre 10 y 1000 kg.");
                return;
            }

            var user = _dbContext.Users.Find(CurrentUser.LoggedInUserId);

            if (user != null)
            {
                if (NewWeight == user.Weight && NewHeight == (int)user.Height &&
                    NewGoal == (double)user.GoalWeight && NewAge == (int)user.Age &&
                    user.ObjectiveID == NewObjective)
                {
                    ShowNotification("Cambia algún dato.");
                    return;
                }

                // Guardar los valores actuales antes de actualizarlos

                var weightHistory = new WeightHistory
                {
                    UserID = user.ID,
                    Weight = user.Weight ?? 0,
                    Date = DateOnly.FromDateTime(DateTime.Now)
                };

                _dbContext.Weights.Add(weightHistory);

                user.Weight = NewWeight;
                user.Height = NewHeight;
                user.GoalWeight = NewGoal;
                user.Age = NewAge;
                user.ObjectiveID = NewObjective;

                _dbContext.SaveChanges();

                ShowSuccess("Datos actualizados correctamente.");
            }
            else
            {
                ShowError("Error: Usuario no encontrado.");
            }
        }
        catch (Exception ex)
        {
            ShowError($"Error al actualizar los datos: {ex.Message}");
        }
    }


    // Método para eliminar todos los datos del usuario
    private void DeleteAllData()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var result = MessageBox.Show(
                Application.Current.MainWindow,
                "¿Estás seguro de que deseas eliminar todas las comidas y ejercicios?",
                "Confirmación de eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var meals = _dbContext.Meals.Where(m => m.UserID == CurrentUser.LoggedInUserId).ToList();
                    var exercises = _dbContext.Exercises.Where(e => e.UserID == CurrentUser.LoggedInUserId).ToList();

                    _dbContext.Meals.RemoveRange(meals);
                    _dbContext.Exercises.RemoveRange(exercises);
                    _dbContext.SaveChanges();

                    ShowSuccess("Todas las comidas y ejercicios se eliminaron correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar los datos: {ex.Message}");
                }
            }
            else
            {
                ShowWarning("La operación ha sido cancelada.");
            }
        });
    }
}
