namespace HomeAutomation
{
    public partial class TasksPage : ContentPage
    {
        public TasksPage()
        {
            InitializeComponent();
        }

        private void OnAddNewTaskClicked(object sender, EventArgs e)
        {
            var taskFrame = CreateTaskWidget();
            TaskContainer.Children.Add(taskFrame);
        }

        private Frame CreateTaskWidget()
        {
            var taskFrame = new Frame
            {
                Padding = 10,
                Margin = new Thickness(10),
                CornerRadius = 10,
                HasShadow = true,
                BackgroundColor = Color.FromHex("#1EE3CF")
            };

            var flipGrid = new Grid();

            // Front Side - Task Configuration UI
            var frontSide = new StackLayout { Padding = 10 };

            // Trigger Picker (Time-based, Light-Sensor, Other)
            var triggerLabel = new Label { Text = "Trigger", FontSize = 18 };
            var triggerPicker = new Picker { Title = "Select Trigger" };
            triggerPicker.Items.Add("Time-based");
            triggerPicker.Items.Add("Light-Sensor");
            triggerPicker.Items.Add("Other");
            triggerPicker.SelectedIndexChanged += (s, e) => OnTriggerChanged(triggerPicker, frontSide);

            // Add Trigger Picker to the front side
            frontSide.Children.Add(triggerLabel);
            frontSide.Children.Add(triggerPicker);

            // Weekdays Section
            var weekdaysLabel = new Label { Text = "Weekdays", FontSize = 18 };
            var weekdaysStack = new StackLayout { Orientation = StackOrientation.Horizontal };

            // Weekday Checkboxes
            var mondayCheckBox = new CheckBox();
            var mondayLabel = new Label { Text = "Monday" };
            var tuesdayCheckBox = new CheckBox();
            var tuesdayLabel = new Label { Text = "Tuesday" };
            var wednesdayCheckBox = new CheckBox();
            var wednesdayLabel = new Label { Text = "Wednesday" };
            var thursdayCheckBox = new CheckBox();
            var thursdayLabel = new Label { Text = "Thursday" };
            var fridayCheckBox = new CheckBox();
            var fridayLabel = new Label { Text = "Friday" };
            var saturdayCheckBox = new CheckBox();
            var saturdayLabel = new Label { Text = "Saturday" };
            var sundayCheckBox = new CheckBox();
            var sundayLabel = new Label { Text = "Sunday" };

            weekdaysStack.Children.Add(mondayCheckBox);
            weekdaysStack.Children.Add(mondayLabel);
            weekdaysStack.Children.Add(tuesdayCheckBox);
            weekdaysStack.Children.Add(tuesdayLabel);
            weekdaysStack.Children.Add(wednesdayCheckBox);
            weekdaysStack.Children.Add(wednesdayLabel);
            weekdaysStack.Children.Add(thursdayCheckBox);
            weekdaysStack.Children.Add(thursdayLabel);
            weekdaysStack.Children.Add(fridayCheckBox);
            weekdaysStack.Children.Add(fridayLabel);
            weekdaysStack.Children.Add(saturdayCheckBox);
            weekdaysStack.Children.Add(saturdayLabel);
            weekdaysStack.Children.Add(sundayCheckBox);
            weekdaysStack.Children.Add(sundayLabel);

            frontSide.Children.Add(weekdaysLabel);
            frontSide.Children.Add(weekdaysStack);

            // Output Picker
            var outputLabel = new Label { Text = "Output", FontSize = 18 };
            var outputPicker = new Picker { Title = "Select Output" };
            outputPicker.Items.Add("Turn On Light");
            outputPicker.Items.Add("Turn Off Light");
            outputPicker.Items.Add("Activate Device");
            outputPicker.Items.Add("Deactivate Device");

            frontSide.Children.Add(outputLabel);
            frontSide.Children.Add(outputPicker);

            // Back Side - Task Details
            var backSide = new StackLayout { Padding = 10, IsVisible = false };

            var taskDetailsLabel = new Label { Text = "Task Details", FontSize = 18 };
            backSide.Children.Add(taskDetailsLabel);

            // URL Display for the Task
            var taskUrlLabel = new Label { Text = "Cam URL: https://127:0.0.1:5000", FontSize = 14 };
            backSide.Children.Add(taskUrlLabel);

            // Add Settings: Change Title, Change Color, Add Note
            var titleLabel = new Label { Text = "Change Title", FontSize = 18 };
            var titleEntry = new Entry { Text = "Task Title", Placeholder = "Enter Title" };
            backSide.Children.Add(titleLabel);
            backSide.Children.Add(titleEntry);

            var colorLabel = new Label { Text = "Change Color", FontSize = 18 };
            var colorPicker = new Picker { Title = "Select Color" };
            colorPicker.Items.Add("Green");
            colorPicker.Items.Add("Blue");
            colorPicker.Items.Add("Red");
            colorPicker.SelectedIndexChanged += (s, e) =>
            {
                switch (colorPicker.SelectedItem.ToString())
                {
                    case "Green":
                        taskFrame.BackgroundColor = Colors.Green;
                        break;
                    case "Blue":
                        taskFrame.BackgroundColor = Colors.Blue;
                        break;
                    case "Red":
                        taskFrame.BackgroundColor = Colors.Red;
                        break;
                    default:
                        taskFrame.BackgroundColor = Color.FromHex("#1EE3CF");
                        break;
                }
            };
            backSide.Children.Add(colorLabel);
            backSide.Children.Add(colorPicker);

            var noteLabel = new Label { Text = "Add a Note", FontSize = 18 };
            var noteEntry = new Entry { Text = "", Placeholder = "Enter a note for the task" };
            backSide.Children.Add(noteLabel);
            backSide.Children.Add(noteEntry);

            // Back Button to flip back to the front
            var backButton = new Button { Text = "Back" };
            backButton.Clicked += (s, e) =>
            {
                frontSide.IsVisible = true;
                backSide.IsVisible = false;
            };
            backSide.Children.Add(backButton);

            // Flip functionality
            var flipButton = new Button { Text = "Flip to Details" };
            flipButton.Clicked += (s, e) =>
            {
                frontSide.IsVisible = false;
                backSide.IsVisible = true;
            };

            frontSide.Children.Add(flipButton);

            // Add front and back sides to the flip grid
            flipGrid.Children.Add(frontSide);
            flipGrid.Children.Add(backSide);

            taskFrame.Content = flipGrid;

            return taskFrame;
        }

        private void OnTriggerChanged(Picker triggerPicker, StackLayout frontSide)
        {
            // Show specific controls based on the selected trigger
            if (triggerPicker.SelectedItem.ToString() == "Time-based")
            {
                var timePicker = new TimePicker();
                frontSide.Children.Add(timePicker);
            }
            else
            {
                // Clear Time Picker if not Time-based
                var timePicker = frontSide.Children.OfType<TimePicker>().FirstOrDefault();
                if (timePicker != null)
                    frontSide.Children.Remove(timePicker);
            }
        }
    }
}
