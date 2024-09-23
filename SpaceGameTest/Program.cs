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
        static int gameTimeStep = 0;

        static int year = 0;
        static int month = 0;
        static int day = 0;
        static int hour = 0;
        static int minute = 0;

        private Label displayLabel;

        public SpaceGame()
        {
            // Set up the form
            // this.Text = "Space Game";
            // this.ClientSize = new System.Drawing.Size(400, 400); // Square size
            // this.BackColor = System.Drawing.Color.Black;

            // displayLabel = new Label();
            // displayLabel.Text = "I am on this screen";
            // displayLabel.ForeColor = System.Drawing.Color.White;
            // displayLabel.AutoSize = true;
            // displayLabel.Location = new System.Drawing.Point(150, 180); // Center the text
            // this.Controls.Add(displayLabel);

            // Start the game logic
            StartNewGame();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new SpaceGame()); // Display the form
        }

        static void StartNewGame()
        {
            Console.WriteLine("---Starting game loop---");
            Console.WriteLine("---create ship---");
            Ship ship = new Ship("SS Voyager", 10); // Create the ship with crew size
            Console.WriteLine("---create users---");
            List<User> users = createCharacters(10, ship);
            Console.WriteLine("---finished creating---");

            while (true)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                try
                {
                    ProcessGameTick(users, ship); // Pass the ship here
                    FTProcessGameTick(stopwatch);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }

                // Update game time step
                gameTimeStep++;
                UpdateAndDisplayGameTime();
            }
        }

        static List<User> createCharacters(int numberOfCrew, Ship ship)
        {
            List<User> users = new List<User>();
            int roomIndex = 0; // Start from the first crew room

            for (int i = 0; i < numberOfCrew; i++)
            {
                string name = $"UserFirstName{i}";
                string lastName = $"UserLastName{i}";

                // Allocate a unique room number for the user
                Room userRoom = ship.Rooms[roomIndex++];
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

            foreach (User user in users)
            {
                user.DisplayUserInfo();
            }
            return users;
        }

        static void ProcessGameTick(List<User> users, Ship ship)
        {
            ship.PrintRoomLocations();
            Console.WriteLine($"{"Name",-20} {"LastName",-20} {"Title",-15} {"Health",5} {"Stamina",7} {"Hunger",7} {"SleepLevel",12} {"CurrentLocation",15} {"status",13}");
            
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
                else
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
                    
                    if (user.Hunger <= 0)
                    {
                        user.ChangeStatus("Working");
                        user.CurrentLocation = user.WorkLocation; // Return to work location
                    }
                    user.DisplayUserInfo();
                }
                // Handle sleeping
                else if (user.Status == "Sleeping")
                {
                    user.Sleep();
                    user.DecreaseHunger(); // Even while sleeping, decrease hunger
                    
                    if (user.SleepLevel >= 100)
                    {
                        user.ChangeStatus("Working");
                        user.CurrentLocation = user.WorkLocation; // Return to work location
                    }
                    user.DisplayUserInfo();
                }
                // Default working status
                else if (user.Status == "Working")
                {
                    user.DecreaseHunger();
                    user.DecreaseSleep();
                    user.DisplayUserInfo();
                }
            }
        }

        static void FTProcessGameTick(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            int timeRemaining = RealTimeStep - (int)stopwatch.ElapsedMilliseconds;
            if (timeRemaining > 0)
            {
                Thread.Sleep(timeRemaining);
            }
        }

        static void UpdateAndDisplayGameTime()
        {
            minute += GameTimeStep;

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

            Console.WriteLine($"Year: {year}, Month: {month}, Day: {day}, Hour: {hour}, Minute: {minute}");
        }
    }
}
