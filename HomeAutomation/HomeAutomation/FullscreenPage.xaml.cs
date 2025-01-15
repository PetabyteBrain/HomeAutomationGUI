namespace HomeAutomation
{
    public partial class FullscreenPage : ContentPage
    {
        public FullscreenPage()
        {
            InitializeComponent();
        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            // Close the fullscreen page and return to the previous page
            await Navigation.PopAsync();
        }
    }
}
