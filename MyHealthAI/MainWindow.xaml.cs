
using System.Windows;
using System.Windows.Controls;

namespace MyHealthAI
{
    public partial class MainWindow : Window
    {
        bool logout = false;

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
            logout = true;
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!logout)
            {
                base.OnClosed(e);
                Application.Current.Shutdown();
            }
        }

    }
}
