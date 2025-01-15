namespace HomeAutomation
{
    public partial class GroupsPage : ContentPage
    {
        private List<Group> groups = new List<Group>();
        private List<User> users = new List<User>();

        private User selectedUser; // Store the selected user for editing

        public GroupsPage()
        {
            InitializeComponent();
            LoadGroups();
        }

        private void LoadGroups()
        {
            // Example groups and users data
            groups = new List<Group>
            {
                new Group { Name = "Group 1", UserCount = 2, DeviceCount = 3 },
                new Group { Name = "Group 2", UserCount = 3, DeviceCount = 4 }
            };

            // Bind Pickers and Table with data
            groupPicker.ItemsSource = groups.Select(g => g.Name).ToList();
            userGroupPicker.ItemsSource = groups.Select(g => g.Name).ToList();
            groupsTable.ItemsSource = groups;
        }

        private void OnGroupSelected(object sender, EventArgs e)
        {
            var selectedGroupName = groupPicker.SelectedItem?.ToString();
            var selectedGroup = groups.FirstOrDefault(g => g.Name == selectedGroupName);
            if (selectedGroup != null)
            {
                groupNameEntry.Text = selectedGroup.Name;
            }
        }

        private void OnRenameGroupClicked(object sender, EventArgs e)
        {
            var newGroupName = groupNameEntry.Text;
            var selectedGroupName = groupPicker.SelectedItem?.ToString();
            var selectedGroup = groups.FirstOrDefault(g => g.Name == selectedGroupName);

            if (selectedGroup != null && !string.IsNullOrEmpty(newGroupName))
            {
                selectedGroup.Name = newGroupName;
                LoadGroups();
                groupNameEntry.Text = string.Empty;
                Console.WriteLine($"Group name updated to: {newGroupName}");
            }
        }

        private void OnUserGroupSelected(object sender, EventArgs e)
        {
            var selectedGroup = userGroupPicker.SelectedItem?.ToString();
            if (selectedGroup != null)
            {
                var group = groups.FirstOrDefault(g => g.Name == selectedGroup);
                if (group != null)
                {
                    users = new List<User>
                    {
                        new User { Username = "user1", FirstName = "John", LastName = "Doe" },
                        new User { Username = "user2", FirstName = "Jane", LastName = "Smith" }
                    };
                    usersInGroupList.ItemsSource = users;
                }
            }
        }

        private void OnEditUserClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.BindingContext as User;

            if (user != null)
            {
                selectedUser = user;
                editUsernameEntry.Text = user.Username;
                editFirstNameEntry.Text = user.FirstName;
                editLastNameEntry.Text = user.LastName;
            }
        }

        private void OnSaveUserChangesClicked(object sender, EventArgs e)
        {
            if (selectedUser != null)
            {
                selectedUser.Username = editUsernameEntry.Text;
                selectedUser.FirstName = editFirstNameEntry.Text;
                selectedUser.LastName = editLastNameEntry.Text;

                usersInGroupList.ItemsSource = null; // Clear the list
                usersInGroupList.ItemsSource = users; // Refresh the list with updated data

                editUsernameEntry.Text = string.Empty;
                editFirstNameEntry.Text = string.Empty;
                editLastNameEntry.Text = string.Empty;
                Console.WriteLine($"User updated: {selectedUser.Username}");
            }
        }

        private void OnDeleteUserClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.BindingContext as User;

            if (user != null)
            {
                users.Remove(user);
                usersInGroupList.ItemsSource = null;
                usersInGroupList.ItemsSource = users;
                Console.WriteLine($"Deleted user: {user.Username}");
            }
        }

        private void OnCreateGroupClicked(object sender, EventArgs e)
        {
            var newGroupTitle = newGroupEntry.Text;

            if (!string.IsNullOrEmpty(newGroupTitle))
            {
                // Generate random number of users and devices (between 0 and 10)
                Random random = new Random();
                int randomUserCount = random.Next(0, 11);  // Random number between 0 and 10
                int randomDeviceCount = random.Next(0, 11);  // Random number between 0 and 10

                // Create new group with random user and device counts
                var newGroup = new Group
                {
                    Name = newGroupTitle,
                    UserCount = randomUserCount,
                    DeviceCount = randomDeviceCount
                };

                // Add new group to the list
                groups.Add(newGroup);

                // Rebind the group picker and table to the updated groups list
                groupPicker.ItemsSource = null;
                groupPicker.ItemsSource = groups.Select(g => g.Name).ToList();  // Update picker items

                groupsTable.ItemsSource = null;
                groupsTable.ItemsSource = groups;  // Update the table view

                // Reset the new group entry field
                newGroupEntry.Text = string.Empty;

                Console.WriteLine($"Created new group: {newGroupTitle}, Users: {randomUserCount}, Devices: {randomDeviceCount}");
            }
        }
    }

    public class Group
    {
        public string Name { get; set; }
        public int UserCount { get; set; }
        public int DeviceCount { get; set; }
    }

    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
