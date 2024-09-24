using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using SpaceGameUser;
using SpaceGameShip;
using SpaceGameRoom;
using SpaceGameSystems;

namespace SpaceGame
{
    public class SpaceGame : Form
    {
        static readonly int RealTimeStep = 2000;
        static int year = 0;
        static int month = 0;
        static int day = 0;
        static int hour = 0;
        static int minute = 0;

        private Label displayLabel;
        private System.Windows.Forms.Timer gameTimer;

        Ship ship;
        List<User> users;
        List<SpaceSystem> systems;
        GameSetup gameSetup;

        public SpaceGame()
        {
            this.Text = "Space Game";
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.BackColor = System.Drawing.Color.Black;

            displayLabel = new Label();
            displayLabel.ForeColor = System.Drawing.Color.White;
            displayLabel.AutoSize = true;
            displayLabel.Location = new System.Drawing.Point(10, 10);
            this.Controls.Add(displayLabel);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = RealTimeStep;
            gameTimer.Tick += OnGameTick;

            gameSetup = new GameSetup();
            CreateNewGame();
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
            systems = gameSetup.CreateSystems(10);
            gameTimer.Start();
        }

        private void OnGameTick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                ProcessGameTick(users, ship);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            gameSetup.UpdateAndDisplayGameTime(ref year, ref month, ref day, ref hour, ref minute, displayLabel);
            UpdateDisplay();

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

        void UpdateDisplay()
        {
            string userInfoHeader = $"{"Name",-20} {"LastName",-20} {"Title",-24} {"Hunger",10} {"SleepLevel",12} {"CurrentLocation",15} {"Status",15}\n";
            string userInfo = "";
            foreach (var user in users)
            {
                userInfo += $"{user.Name,-20} {user.LastName,-20} {user.Title,-24} {user.Hunger,10} {user.SleepLevel,12} {user.CurrentLocation,15} {user.Status,15}\n";
            }
            displayLabel.Text = $"Year: {year}, Month: {month}, Day: {day}, Hour: {hour}, Minute: {minute}\n\n" + userInfoHeader + userInfo;
        }
    }
}
