using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyHealthAI.Services
{
    public interface IWindowService
    {
        void ShowWindow<T>() where T : Window, new();
        void CloseWindow(Window window);
    }

    public class WindowService : IWindowService
    {
        public void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }

        public void CloseWindow(Window window)
        {
            window?.Close();
        }
    }

}
