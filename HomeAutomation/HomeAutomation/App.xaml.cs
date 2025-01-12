namespace HomeAutomation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public void ChangeColors(Color menuColor, Color titleBarColor)
        {
            // Change the colors dynamically
            Application.Current.Resources["MenuBackgroundColor"] = menuColor;
            Application.Current.Resources["TitleBarColor"] = titleBarColor;
        }
    }
}
