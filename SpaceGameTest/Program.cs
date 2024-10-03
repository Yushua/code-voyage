using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using SpaceGameUser;
using SpaceGameShip;
using SpaceGameRoom;
using universeCreate;
using systemCreate;

namespace SpaceGame
{
        public class SpaceGame : Form
        {
        static readonly int RealTimeStep = 1000;
        static int year = 0;
        static int month = 0;
        static int day = 0;
        static int hour = 0;
        static int minute = 0;

        bool engineStatus = false;
        bool mapUpdate = false;

        private Panel block1;
        private Panel block2;
        private Panel block3;
        private Panel block4;
        private Panel block5; // New panel for the full-size map

        private System.Windows.Forms.Timer gameTimer;
        private Ship ship;
        private List<User> users;
        private universeCreate.Universe universe;
        private GameSetup gameSetup;
        private char[,] universeMap = new char[61, 61];
        private int mapWidth = 61;
        private int mapHeight = 61;
        private List<StarSystem> StarSystemList;
        int mapX = 0;
        int mapy = 0;
        public SpaceGame()
        {
            this.Text = "Space Game";
            this.ClientSize = new System.Drawing.Size(3800, 1000);
            this.BackColor = System.Drawing.Color.Black;

            InitializePanels();

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = RealTimeStep;
            gameTimer.Tick += OnGameTick;

            gameSetup = new GameSetup();
            CreateNewGame();
        }

        private void InitializePanels()
        {
            // Left side panels (2x2 grid)
            block1 = CreatePanel(0, 0, 950, 300);
            block2 = CreatePanel(950, 0, 950, 300);
            block3 = CreatePanel(0, 300, 950, 700);
            block4 = CreatePanel(950, 300, 950, 700);
            block5 = CreatePanel(1915, 0, 1915, 1000);
        }

        private Panel CreatePanel(int x, int y, int width, int height)
        {
            Panel panel = new Panel
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(width, height),
                BackColor = System.Drawing.Color.Gray
            };

            this.Controls.Add(panel);
            return panel;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new SpaceGame());
        }

        void CreateNewGame()
        {
            ship = new Ship("SS Voyager", 10);
            users = gameSetup.CreateCharacters(10, ship);
            universe = new universeCreate.Universe(10000, 10000);
            StarSystemList = new List<StarSystem>();
            CreateStarSystemsList();
            CreateUniverseMap();
            DisplayMap();
            DisplayListSystems();            
            gameTimer.Start();
        }

        private void OnGameTick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try { ProcessGameTick(users, ship); }
            catch (Exception ex) { Console.WriteLine($"Error occurred: {ex.Message}"); }

            UpdateAndDisplayGameTime(ref year, ref month, ref day, ref hour, ref minute, block1);
            UpdateDisplayCrew();

            if (mapUpdate)
            {
                //first set map begin and end
                //then make sure this display ebgin and end have one thing. to assure where to place the markers on the map.
                
                CreateUniverseMap();
                DisplayMap();
                DisplayListSystems();
                mapUpdate = false;
            }
            stopwatch.Stop();
            int timeRemaining = RealTimeStep - (int)stopwatch.ElapsedMilliseconds;
            if (timeRemaining > 0)
            {
                Thread.Sleep(timeRemaining);
            }
        }

        void ProcessGameTick(List<User> users, Ship ship)
        {
            ship.PrintRoomLocations();
            foreach (var user in users)
            {
                if (user.Hunger > 70 && user.Status != "Eating" && user.Status != "Sleeping")
                {
                    user.CurrentLocation = ship.Canteen.RoomNumber;
                    user.ChangeStatus("Eating");
                }
                else if (user.SleepLevel < 20 && user.Status != "Sleeping" && user.Status != "Eating")
                {
                    user.CurrentLocation = user.CurrentLocation;
                    user.ChangeStatus("Sleeping");
                }
                else if (user.Status != "Eating" && user.Status != "Sleeping")
                {
                    user.CurrentLocation = user.WorkLocation;
                    user.ChangeStatus("Working");
                }

                if (user.Status == "Eating")
                {
                    user.Eat();
                    user.DecreaseSleep();
                    if (user.Hunger <= 0)
                    {
                        user.ChangeStatus("Working");
                        user.CurrentLocation = user.WorkLocation;
                    }
                }
                else if (user.Status == "Sleeping")
                {
                    user.Sleep();
                    user.DecreaseHunger();
                    if (user.SleepLevel >= 100)
                    {
                        user.ChangeStatus("Working");
                        user.CurrentLocation = user.WorkLocation;
                    }
                }
                else if (user.Status == "Working")
                {
                    user.DecreaseHunger();
                    user.DecreaseSleep();
                }
            }
        }

        private void CreateStarSystemsList(){
            StarSystemList.Clear();
            int rangeX = mapWidth / 2;
            int rangeY = mapHeight / 2;

            int shipX = ship.GetPositionX();
            int shipY = ship.GetPositionY();

            foreach (var system in universe.GetStarSystems())
            {
                if (system.PositionX >= shipX - rangeX && system.PositionX <= shipX + rangeX &&
                    system.PositionY >= shipY - rangeY && system.PositionY <= shipY + rangeY)
                {
                    StarSystemList.Add(system);
                }
            }
        }

        private void CreateUniverseMap()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    universeMap[x, y] = ' ';
                }
            }

            int shipX = mapWidth / 2;
            int shipY = mapHeight / 2;
            universeMap[shipX, shipY] = 'S';

            foreach (var system in StarSystemList)
            {
                int systemX = (int)system.PositionX;
                int systemY = (int)system.PositionY;

                if (systemX >= 0 && systemX < mapWidth && systemY >= 0 && systemY < mapHeight)
                {
                    universeMap[systemX, systemY] = 'B';
                }
            }
        }

        public void MoveShip(int deltaX, int deltaY)
        {
            //move the ship
            /*
                1. it moves until its there
                2/ it pauses
                3/ the ship is destroyed
            */
        }

        void UpdateDisplayCrew()
        {
            string userInfoHeader = $"{"Name",-20} {"LastName",-20} {"Title",-24} {"Hunger",10} {"SleepLevel",12} {"CurrentLocation",15} {"Status",15}\n";
            string userInfo = "";
            foreach (var user in users)
            {
                userInfo += $"{user.Name,-20} {user.LastName,-20} {user.Title,-24} {user.Hunger,10} {user.SleepLevel,12} {user.CurrentLocation,15} {user.Status,15}\n";
            }

            block1.Controls.Clear();

            Label crewDisplay = new Label
            {
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = true,
                Location = new System.Drawing.Point(10, 10),
                Text = $"Year: {year}, Month: {month}, Day: {day}, Hour: {hour}, Minute: {minute}\n\n" + userInfoHeader + userInfo
            };

            block1.Controls.Add(crewDisplay);
            block1.Refresh();
        }

        void DisplayMap()
        {
            block5.Controls.Clear(); // Use block5 for the map display

            string mapDisplay = "";

            for (int y = 0; y < mapHeight; y++)
            {
                // Displaying the y-coordinate with padding, and adding brackets around the map row
                mapDisplay += $"{y.ToString().PadLeft(2, '0')} [ ";

                for (int x = 0; x < mapWidth; x++)
                {
                    mapDisplay += $"{universeMap[x, y]} ";
                }
                mapDisplay += "]\n"; // Close the row
            }

            Label mapLabel = new Label
            {
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = false,
                Size = new System.Drawing.Size(1900, block5.Height), // Adjust size for block5
                Text = mapDisplay,
                Font = new System.Drawing.Font("Courier New", 8),
                TextAlign = System.Drawing.ContentAlignment.TopLeft
            };

            mapLabel.Height = mapLabel.PreferredHeight; // Set the label height dynamically based on content

            block5.Controls.Add(mapLabel);
            block5.AutoScroll = true; // Enable scrolling if content overflows
            block5.Refresh();
        }
        void DisplayListSystems()
        {
            block4.Controls.Clear();
            string systemsDisplay = StarSystemList.Count > 0 
                ? $"Systems Found: {StarSystemList.Count}\n" 
                : "No systems found.\n";

            foreach (var system in StarSystemList.Take(20))
            {
                systemsDisplay += $"System Name: {system.Name}, Coordinates: ({system.PositionX}, {system.PositionY}), Planets: {system.GetPlanetSize()}\n";
            }

            Label systemsLabel = new Label
            {
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = true,  // Allow the label to auto-size based on content
                Location = new System.Drawing.Point(0, 0), // Position at the top-left corner of block4
                Text = systemsDisplay,
                Font = new System.Drawing.Font("Courier New", 8),
                TextAlign = System.Drawing.ContentAlignment.TopLeft // Align text to top-left
            };

            Panel containerPanel = new Panel
            {
                AutoScroll = true, // Enable scrolling for the container panel
                Location = new System.Drawing.Point(0, 0),
                Size = block4.Size, // Make sure the container panel matches the size of block4
                BackColor = System.Drawing.Color.Transparent // Transparent to blend with the parent panel
            };

            containerPanel.Controls.Add(systemsLabel);
            block4.Controls.Add(containerPanel);
            block4.Refresh();
        }

        public void UpdateAndDisplayGameTime(ref int year, ref int month, ref int day, ref int hour, ref int minute, Panel displayPanel)
        {
            minute += 5;

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
