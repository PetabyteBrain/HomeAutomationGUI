using Microsoft.Maui.Controls;

namespace HomeAutomation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Here you would typically validate the username and password
            // For demonstration, let's assume any non-empty input is valid
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                // Navigate to HomePage after successful login
                await Shell.Current.GoToAsync("Home");
            }
            else
            {
                await DisplayAlert("Login Failed", "Please enter valid credentials.", "OK");
            }
        }

    }
}
