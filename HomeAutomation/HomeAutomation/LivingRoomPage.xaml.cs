using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace HomeAutomation
{
    public partial class LivingRoomPage : ContentPage
    {
        private bool isFlipped = false;

        public LivingRoomPage()
        {
            InitializeComponent();
        }

        private async void OnMenuButtonTapped(object sender, EventArgs e)
        {
            isFlipped = !isFlipped;

            // Flip animation
            var rotation = isFlipped ? 360 : 0;
            await FlipGrid.RotateTo(rotation, 300, Easing.CubicInOut);

            // Toggle visibility of front and back sides
            FrontSide.IsVisible = !isFlipped;
            BackSide.IsVisible = isFlipped;

            // Reset rotation to 0 for the next flip
            if (!isFlipped)
            {
                FlipGrid.Rotation = 0;
            }
        }
        private void OnBackButtonTapped(object sender, EventArgs e)
        {
            // Update the widget title based on the entry
            var widgetTitle = TitleEntry.Text;
            var titleLabel = FrontSide.Children.OfType<Label>().FirstOrDefault(l => l.Text == "Light1"); // Assuming "Light1" is the initial title
            if (titleLabel != null)
            {
                titleLabel.Text = widgetTitle; // Update the title label
            }

            // Flip back to the front side
            isFlipped = false;
            FrontSide.IsVisible = true;
            BackSide.IsVisible = false;
            FlipGrid.Rotation = 0; // Reset rotation
        }

        private void OnEntryTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var selectedType = (sender as RadioButton)?.Content.ToString();
                if (selectedType == "Switch")
                {
                    // Show the switch and hide the radio buttons
                    ControlSwitch.IsVisible = true;
                    RadioButtonStack.IsVisible = false;
                }
                else if (selectedType == "Radio Buttons")
                {
                    // Show the radio buttons and hide the switch
                    ControlSwitch.IsVisible = false;
                    RadioButtonStack.IsVisible = true;
                }
            }
        }

        private void OnIconSelectionChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var selectedIcon = (sender as RadioButton)?.Content.ToString();
                Image iconImage = WidgetContentView.Children.OfType<Image>().FirstOrDefault();

                if (iconImage != null)
                {
                    // Update the widget icon based on selectedIcon
                    switch (selectedIcon)
                    {
                        case "Icon 1":
                            iconImage.Source = "lightbulb_icon.png"; // Ensure the image name is correct
                            break;
                        case "Icon 2":
                            iconImage.Source = "blinds_icon.png"; // Ensure the image name is correct
                            break;
                        case "Icon 3":
                            iconImage.Source = "heater_icon.png"; // Ensure the image name is correct
                            break;
                    }
                }
            }
        }

        private void OnColorSelectionChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var selectedColor = (sender as RadioButton)?.Content.ToString();
                Color backgroundColor;

                switch (selectedColor)
                {
                    case "Red":
                        backgroundColor = Colors.Red;
                        break;
                    case "Green":
                        backgroundColor = Colors.Green;
                        break;
                    case "Blue":
                        backgroundColor = Colors.Blue;
                        break;
                    default:
                        backgroundColor = Colors.Gray; // Default color
                        break;
                }

                // Update the background color of the widget
                WidgetContentView.BackgroundColor = backgroundColor;
                ColorPreview.BackgroundColor = backgroundColor; // Update the preview as well
            }
        }
        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // Logic for when the switch is turned ON
                Console.WriteLine("Switch is ON");
                // You can add additional logic here, such as changing the icon or performing an action
            }
            else
            {
                // Logic for when the switch is turned OFF
                Console.WriteLine("Switch is OFF");
                // You can add additional logic here, such as changing the icon or performing an action
            }
        }
        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var selectedRadioButton = sender as RadioButton;
                if (selectedRadioButton != null)
                {
                    // Logic to handle the state change
                    if (selectedRadioButton == RadioButtonOn)
                    {
                        // Handle the "On" state
                        Console.WriteLine("Radio Button is ON");
                        // You can add additional logic here, such as changing the icon or performing an action
                    }
                    else if (selectedRadioButton == RadioButtonOff)
                    {
                        // Handle the "Off" state
                        Console.WriteLine("Radio Button is OFF");
                        // You can add additional logic here, such as changing the icon or performing an action
                    }
                }
            }
        }

    }
}
