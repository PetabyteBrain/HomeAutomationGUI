namespace HomeAutomation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Set the initial page to MainPage
            CurrentItem = Items[0]; // Assuming MainPage is the first FlyoutItem
            // Define routes for navigation
            Routing.RegisterRoute("Home", typeof(HomePage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
        }
    }
}
