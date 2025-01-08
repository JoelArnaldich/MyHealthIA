using System;
using System.Threading.Tasks;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object> _executeWithParameter;
    private readonly Action _executeWithoutParameter;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _executeWithParameter = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _executeWithoutParameter = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute == null ? (Predicate<object>)null : new Predicate<object>(_ => canExecute());
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    public async void Execute(object parameter)
    {
        if (CanExecute(parameter))
        {
            if (_executeWithParameter != null)
                await Task.Run(() => _executeWithParameter(parameter));
            else
                await Task.Run(() => _executeWithoutParameter());
        }
    }
}
