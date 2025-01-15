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
            WidthRequest = 250, // Increase size of the widget
            HeightRequest = 250, // Increase size of the widget
            Margin = new Thickness(10),
            Padding = 10,
            CornerRadius = 10,
            HasShadow = true,
            BackgroundColor = Color.FromHex("#1EE3CF") // Set the background color here
        };

        var flipGrid = new Grid();

        // Declare the front side of the widget first
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

        // Radio buttons for input type
        var inputTypeStack = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center
        };

        var radioButton1 = new RadioButton
        {
            Content = "Option 1",
            GroupName = "InputType",
            IsChecked = true
        };

        var radioButton2 = new RadioButton
        {
            Content = "Option 2",
            GroupName = "InputType"
        };

        inputTypeStack.Children.Add(radioButton1);
        inputTypeStack.Children.Add(radioButton2);
        backContent.Children.Add(inputTypeStack);

        // Color selection buttons
        var colorButtonStack = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center
        };

        var colorButton1 = new Button
        {
            Text = "Red",
            BackgroundColor = Colors.Red
        };
        colorButton1.Clicked += (s, e) => frame.BackgroundColor = Colors.Red;

        var colorButton2 = new Button
        {
            Text = "Green",
            BackgroundColor = Colors.Green
        };
        colorButton2.Clicked += (s, e) => frame.BackgroundColor = Colors.Green;

        var colorButton3 = new Button
        {
            Text = "Blue",
            BackgroundColor = Colors.Blue
        };
        colorButton3.Clicked += (s, e) => frame.BackgroundColor = Colors.Blue;

        colorButtonStack.Children.Add(colorButton1);
        colorButtonStack.Children.Add(colorButton2);
        colorButtonStack.Children.Add(colorButton3);
        backContent.Children.Add(colorButtonStack);

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
            Source = "placeholder.png", // Replace with your camera image
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