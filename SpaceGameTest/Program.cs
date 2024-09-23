using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using SpaceGameUser;
using SpaceGameShip;
using SpaceGameRoom;

namespace SpaceGame
{
    public class SpaceGame : Form
    {
        static readonly int RealTimeStep = 2000; // in milliseconds (2 seconds)
        static int GameTimeStep = 10; // Each step represents 10 minutes of game time.

        static int year = 0;
        static int month = 0;
        static int day = 0;
        static int hour = 0;
        static int minute = 0;

        private Label displayLabel;
        private System.Windows.Forms.Timer gameTimer; // Use explicit reference to System.Windows.Forms.Timer

        Ship ship;
        List<User> users;

        public SpaceGame()
        {
            // Set up the form
            this.Text = "Space Game";
            this.ClientSize = new System.Drawing.Size(800, 600); // Increased size for wider and taller display
            this.BackColor = System.Drawing.Color.Black;

            // Initialize the label
            displayLabel = new Label();
            displayLabel.ForeColor = System.Drawing.Color.White;
            displayLabel.AutoSize = true;
            displayLabel.Location = new System.Drawing.Point(10, 10); // Set a default position
            this.Controls.Add(displayLabel);

            // Initialize the timer
            gameTimer = new System.Windows.Forms.Timer(); // Explicitly specify System.Windows.Forms.Timer
            gameTimer.Interval = RealTimeStep; // Set the timer interval to 2 seconds
            gameTimer.Tick += OnGameTick; // Assign event handler for each tick

            // Start the game after UI is set up
            CreateNewGame();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new SpaceGame()); // Display the form
        }

        // This will run the game initialization logic and start the timer
        void CreateNewGame()
        {
            Console.WriteLine("---Starting game loop---");
            Console.WriteLine("---create ship---");
            ship = new Ship("SS Voyager", 10); // Create the ship with crew size
            Console.WriteLine("---create users---");
            users = CreateCharacters(10, ship);
            Console.WriteLine("---finished creating---");

            // Start the game timer after characters are created
            gameTimer.Start();
        }

        // This is called every 2 seconds (RealTimeStep)
        private void OnGameTick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                ProcessGameTick(users, ship); // Pass the ship here
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            // Update game time step and display time
            UpdateAndDisplayGameTime();

            stopwatch.Stop();
            int timeRemaining = RealTimeStep - (int)stopwatch.ElapsedMilliseconds;
            if (timeRemaining > 0)
            {
                Thread.Sleep(timeRemaining);
            }
        }

    List<User> CreateCharacters(int numberOfCrew, Ship ship)
    {
        List<User> users = new List<User>();
        int roomIndex = 0; // Start from the first crew room

        for (int i = 0; i < numberOfCrew; i++)
        {
            string name = $"UserFirstName{i}";
            string lastName = $"UserLastName{i}";

            // Allocate a unique room number for the user
            Room userRoom = ship.Rooms[roomIndex++];
            
            // Create the User, passing numberOfCrew and room number
            User newUser = new User(name, lastName, numberOfCrew, userRoom.RoomNumber);

            // Set work location based on title
            switch (newUser.Title)
            {
                case "Captain":
                case "Head Engineer":
                case "Head Scientist":
                    newUser.SetWorkLocation(ship.HeadRoom.RoomNumber); // Head room for captain and heads
                    break;
                case "Cook":
                    newUser.SetWorkLocation(ship.Kitchen.RoomNumber); // Kitchen for cooks
                    break;
                default:
                    newUser.SetWorkLocation(userRoom.RoomNumber); // Default to their own room for now
                    break;
            }

            users.Add(newUser);
        }

        return users;
    }

    void ProcessGameTick(List<User> users, Ship ship)
    {
        ship.PrintRoomLocations();
        
        // Header for user information table
        string userInfoHeader = $"{"Name",-20} {"LastName",-20} {"Title",-24} {"Hunger",10} {"SleepLevel",12} {"CurrentLocation",15} {"Status",15}\n";
        string userInfo = ""; // Initialize an empty string to collect user info
        
        foreach (var user in users)
        {
            // Check for eating or sleeping conditions
            if (user.Hunger > 70 && user.Status != "Eating" && user.Status != "Sleeping")
            {
                user.CurrentLocation = ship.Canteen.RoomNumber;
                user.ChangeStatus("Eating");
            }
            else if (user.SleepLevel < 20 && user.Status != "Sleeping" && user.Status != "Eating")
            {
                user.CurrentLocation = user.CurrentLocation; // Return to own quarters
                user.ChangeStatus("Sleeping");
            }
            else if (user.Status != "Eating" && user.Status != "Sleeping")
            {
                // Go to work if not eating or sleeping
                user.CurrentLocation = user.WorkLocation;
                user.ChangeStatus("Working");
            }

            // Handle eating
            if (user.Status == "Eating")
            {
                user.Eat();
                user.DecreaseSleep(); // Even while eating, decrease sleep

                // Check if user is still hungry
                if (user.Hunger <= 0)
                {
                    user.ChangeStatus("Working");
                    user.CurrentLocation = user.WorkLocation; // Return to work location
                }
            }
            // Handle sleeping
            else if (user.Status == "Sleeping")
            {
                user.Sleep();
                user.DecreaseHunger(); // Even while sleeping, decrease hunger

                // Check if user is fully rested
                if (user.SleepLevel >= 100)
                {
                    user.ChangeStatus("Working");
                    user.CurrentLocation = user.WorkLocation; // Return to work location
                }
            }
            else if (user.Status == "Working")
            {
                user.DecreaseHunger();
                user.DecreaseSleep();
            }

            // Append the user's data to the userInfo string
            userInfo += $"{user.Name,-20} {user.LastName,-20} {user.Title,-24} {user.Hunger,10} {user.SleepLevel,12} {user.CurrentLocation,15} {user.Status,15}\n";
        }

        // Combine the header and all user information into one string
        userInfo = userInfoHeader + userInfo;

        // Output userInfo to console for debugging
        Console.WriteLine(userInfo); // Check if data is available

        // Update the label on the form with the combined text
        displayLabel.Text = $"Year: {year}, Month: {month}, Day: {day}, Hour: {hour}, Minute: {minute}\n\n" + userInfo;
    }


        // Update and display the current game time
        void UpdateAndDisplayGameTime()
        {
            minute += 10; // Increase by 10 minutes

            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }
            if (hour >= 24)
            {
                hour = 0;
                day++;
            }
            if (day >= 30)
            {
                day = 0;
                month++;
            }
            if (month >= 12)
            {
                month = 0;
                year++;
            }
        }
    }
}
