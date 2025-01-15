using Notifications.Wpf;
using Notifications.Wpf.Controls;
using System;

namespace MyHealthAI.Services
{
    public class NotificationService
    {
        private readonly INotificationManager _notificationManager;

        // Constructor que recibe el NotificationManager.
        public NotificationService(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        // Método para mostrar una notificación normal (informativa)
        public void ShowNotification(string message, string title = "Notificación")
        {
            // Crea la notificación
            var notification = new NotificationContent
            {
                Title = title,           // Título de la notificación
                Message = message,       // Mensaje de la notificación
                Type = NotificationType.Information  // Tipo de la notificación
            };

            // Muestra la notificación
            _notificationManager.Show(notification);
        }

        // Método para mostrar una notificación de error
        public void ShowError(string message, string title = "Error")
        {
            // Crea la notificación
            var notification = new NotificationContent
            {
                Title = title,           // Título de la notificación
                Message = message,       // Mensaje de la notificación
                Type = NotificationType.Error  // Tipo de la notificación
            };

            // Muestra la notificación
            _notificationManager.Show(notification);
        }

        // Método para mostrar una notificación de éxito
        public void ShowSuccess(string message, string title = "Éxito")
        {
            // Crea la notificación
            var notification = new NotificationContent
            {
                Title = title,           // Título de la notificación
                Message = message,       // Mensaje de la notificación
                Type = NotificationType.Success  // Tipo de la notificación
            };

            // Muestra la notificación
            _notificationManager.Show(notification);
        }

        // Método para mostrar una notificación de advertencia
        public void ShowWarning(string message, string title = "Advertencia")
        {
            // Crea la notificación
            var notification = new NotificationContent
            {
                Title = title,           // Título de la notificación
                Message = message,       // Mensaje de la notificación
                Type = NotificationType.Warning  // Tipo de la notificación
            };

            // Muestra la notificación
            _notificationManager.Show(notification);
        }
    }
}
