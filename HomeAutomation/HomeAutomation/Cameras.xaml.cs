namespace HomeAutomation;

public partial class Cameras : ContentPage
{
    private bool isFlipped = false;
    public Cameras()
	{
		InitializeComponent();

        AddWidgets(2); // Example: Add 5 widgets dynamically
    }
    private void OnAddWidgetTapped(object sender, EventArgs e)
    {
        // Generate a new widget with a unique title
        var newWidget = CreateWidget($"Widget {WidgetContainer.Children.Count + 1}");

        // Add the new widget to the container
        WidgetContainer.Children.Add(newWidget);
    }

    private void AddWidgets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var widgetFrame = CreateWidget($"Cam {i + 1}");
            // Ensure WidgetsContainer exists in your XAML
            WidgetContainer.Children.Add(widgetFrame);
        }
    }

    private Frame CreateWidget(string title)
    {
        var frame = new Frame
        {
            WidthRequest = 250,
            HeightRequest = 250,
            Margin = new Thickness(10),
            Padding = 10,
            CornerRadius = 10,
            HasShadow = true,
            BackgroundColor = Color.FromHex("#1EE3CF") // Initial color
        };

        var flipGrid = new Grid();

        // Declare the front side of the widget
        var frontSide = new Grid
        {
            IsVisible = true
        };

        frontSide.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        frontSide.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        frontSide.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        frontSide.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        // Declare the back side of the widget
        var backSide = new ScrollView
        {
            IsVisible = false,
            BackgroundColor = Colors.LightGray,
            Padding = new Thickness(10)
        };

        var backContent = new StackLayout();

        // Entry to change the widget name
        var nameEntry = new Entry
        {
            Text = title,
            Placeholder = "Enter widget name",
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        var titleLabel = new Label
        {
            Text = title,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            FontSize = 16
        };

        // Update titleLabel text when nameEntry changes
        nameEntry.TextChanged += (s, e) => titleLabel.Text = nameEntry.Text;

        backContent.Children.Add(nameEntry);

        backSide.Content = backContent;

        var backButton = new Button
        {
            Text = "Back"
        };
        backButton.Clicked += (s, e) =>
        {
            // Flip back to the front side
            backSide.IsVisible = false;
            frontSide.IsVisible = true;
        };
        backContent.Children.Add(backButton);

        // Add Title Label to the frontSide
        frontSide.Children.Add(titleLabel);
        Grid.SetRow(titleLabel, 0);
        Grid.SetColumn(titleLabel, 1);

        // Add the camera image instead of the switch and subtitle
        var cameraImage = new Image
        {
            Source = "placeholder.png", // Updated to your new placeholder name
            Aspect = Aspect.AspectFill,
            WidthRequest = 200,
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        frontSide.Children.Add(cameraImage);
        Grid.SetRow(cameraImage, 1);
        Grid.SetColumn(cameraImage, 0);
        Grid.SetColumnSpan(cameraImage, 2);

        // Fullscreen Button
        var fullscreenButton = new Button
        {
            Text = "🔲", // Text icon for fullscreen
            BackgroundColor = Colors.Transparent,
            BorderColor = Colors.Transparent,
            TextColor = Colors.Black,
            FontSize = 24,
            HorizontalOptions = LayoutOptions.EndAndExpand,
            VerticalOptions = LayoutOptions.End
        };

        fullscreenButton.Clicked += async (s, e) =>
        {
            // Navigate to the FullscreenPage
            await Navigation.PushAsync(new FullscreenPage());
        };

        frontSide.Children.Add(fullscreenButton);
        Grid.SetRow(fullscreenButton, 2);
        Grid.SetColumn(fullscreenButton, 1);

        // Add Menu Button to flip the widget
        var menuButton = new Button
        {
            Text = "☰",
            BackgroundColor = Colors.Transparent,
            BorderColor = Colors.Transparent,
            TextColor = Colors.Black,
            FontSize = 24
        };
        menuButton.Clicked += (s, e) =>
        {
            // Flip to the back side
            frontSide.IsVisible = false;
            backSide.IsVisible = true;
        };
        frontSide.Children.Add(menuButton);
        Grid.SetRow(menuButton, 0);
        Grid.SetColumn(menuButton, 0);

        // Add the front and back sides to the flip grid
        flipGrid.Children.Add(frontSide);
        flipGrid.Children.Add(backSide);

        frame.Content = flipGrid;

        // Color buttons for changing widget color
        var colorButtonStack = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center
        };

        // Red color button
        var redButton = new Button
        {
            Text = "Red",
            BackgroundColor = Colors.Red
        };
        redButton.Clicked += (s, e) => frame.BackgroundColor = Colors.Red;

        // Green color button
        var greenButton = new Button
        {
            Text = "Green",
            BackgroundColor = Colors.Green
        };
        greenButton.Clicked += (s, e) => frame.BackgroundColor = Colors.Green;

        // Blue color button
        var blueButton = new Button
        {
            Text = "Blue",
            BackgroundColor = Colors.Blue
        };
        blueButton.Clicked += (s, e) => frame.BackgroundColor = Colors.Blue;

        // Add color buttons to the stack layout
        colorButtonStack.Children.Add(redButton);
        colorButtonStack.Children.Add(greenButton);
        colorButtonStack.Children.Add(blueButton);

        // Add the color button stack to the back content
        backContent.Children.Add(colorButtonStack);

        // Add camera URL text at the end of the back side
        var cameraUrlLabel = new Label
        {
            Text = "Cam URL: https://127.0.0.1:5000",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.EndAndExpand, // Push it to the bottom
            FontSize = 14,
            TextColor = Colors.Black
        };
        backContent.Children.Add(cameraUrlLabel);

        return frame;
    }




    private async void OnMenuButtonTapped(object sender, EventArgs e, Grid flipGrid)
    {
        isFlipped = !isFlipped;

        // Flip animation
        var rotation = isFlipped ? 360 : 0;
        await flipGrid.RotateTo(rotation, 300, Easing.CubicInOut);

        // Toggle visibility logic should be implemented per widget
    }

    private void OnSwitchToggled(object sender, ToggledEventArgs e)
    {
        Console.WriteLine($"Switch toggled to: {(e.Value ? "ON" : "OFF")}");
    }
}