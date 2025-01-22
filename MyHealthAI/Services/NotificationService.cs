using Notifications.Wpf;

namespace MyHealthAI.Services
{
    public class NotificationService
    {
        private readonly INotificationManager _notificationManager;

        public NotificationService(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public void ShowNotification(string message, string title = "Notificación")
        {

            var notification = new NotificationContent
            {
                Title = title,           
                Message = message,       
                Type = NotificationType.Information 
            };


            _notificationManager.Show(notification);
        }


        public void ShowError(string message, string title = "Error")
        {

            var notification = new NotificationContent
            {
                Title = title,          
                Message = message,      
                Type = NotificationType.Error  
            };


            _notificationManager.Show(notification);
        }


        public void ShowSuccess(string message, string title = "Éxito")
        {

            var notification = new NotificationContent
            {
                Title = title,          
                Message = message,     
                Type = NotificationType.Success  
            };


            _notificationManager.Show(notification);
        }


        public void ShowWarning(string message, string title = "Advertencia")
        {
            // Crea la notificación
            var notification = new NotificationContent
            {
                Title = title,          
                Message = message,      
                Type = NotificationType.Warning  
            };

            _notificationManager.Show(notification);
        }
    }
}
