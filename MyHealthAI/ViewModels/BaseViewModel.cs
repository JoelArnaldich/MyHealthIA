using MyHealthAI.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected readonly NotificationService _notificationService;

    // Constructor para recibir NotificationService
    public BaseViewModel(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Método para mostrar una notificación
    protected void ShowNotification(string message, string title = "Notificación")
    {
        _notificationService.ShowNotification(message, title);
    }

    // Otros métodos para mostrar diferentes tipos de notificaciones (errores, advertencias, etc.)
    protected void ShowError(string message, string title = "Error")
    {
        _notificationService.ShowError(message, title);
    }

    protected void ShowSuccess(string message, string title = "Éxito")
    {
        _notificationService.ShowSuccess(message, title);
    }

    protected void ShowWarning(string message, string title = "Advertencia")
    {
        _notificationService.ShowWarning(message, title);
    }
}
