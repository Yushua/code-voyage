using System;
using System.Collections.Generic;

namespace SpaceGameUser
{
    public class User
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; } = "Crew Member";
        public string Belong { get; private set; }
        public List<string> Professions { get; private set; }
        public int Rank { get; private set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
        public string PlaceOfOrigin { get; set; }
        public string SpaceNeeded { get; set; }
        public string Birthday { get; set; }
        public string CombatStyle { get; set; }
        public int Speed { get; set; }
        public int Happiness { get; set; }
        public int Hunger { get; set; }
        public int SleepLevel { get; set; }
        public int CurrentLocation { get; set; }
        public int HungerDecreaser { get; private set; }
        public int SleepDecreaser { get; private set; }
        public string Status { get; private set; }
        public int WorkLocation { get; private set; }

        private static int captainCount = 0;
        private static int headScientistCount = 0;
        private static int headEngineerCount = 0;
        private static int cookCount = 0;
        private static Random random = new Random();

        public User(string name, string lastName, int numberOfCrew, int roomNumber)
        {
            Name = name;
            LastName = lastName;
            Professions = new List<string>();

            AssignTitleAndRank(numberOfCrew);

            Health = random.Next(50, 101);
            Stamina = random.Next(30, 100);
            PlaceOfOrigin = "Earth";
            Belong = "Crew"; //makes sure you are part of the crew. different factions. different belong
            SpaceNeeded = "Small";
            Birthday = "01-01-2000";
            CombatStyle = "Balanced";
            Speed = random.Next(1, 11);
            Happiness = random.Next(1, 101);
            Hunger = 0; // Start with no hunger
            SleepLevel = 100; // Start fully rested
            CurrentLocation = roomNumber; // Assigned room number
            WorkLocation = 2; // Default work location (e.g., Engineering)
            HungerDecreaser = random.Next(1, 5); // Random hunger decrease rate
            SleepDecreaser = random.Next(1, 3); // Random sleep decrease rate
            Status = "Working";
        }

        private void AssignTitleAndRank(int numberOfCrew)
        {
            // Randomly assign a profession and increment counts
            int professionRoll = random.Next(1, 5);
            switch (professionRoll)
            {
                case 1:
                    if (captainCount == 0)
                    {
                        Title = "Captain";
                        Rank = 1;
                        captainCount++;
                    }
                    break;
                case 2:
                    if (headScientistCount == 0)
                    {
                        Title = "Head Scientist";
                        Rank = 2;
                        headScientistCount++;
                    }
                    break;
                case 3:
                    if (headEngineerCount == 0)
                    {
                        Title = "Head Engineer";
                        Rank = 3;
                        headEngineerCount++;
                    }
                    break;
                case 4:
                    // Check if we can add another cook based on crew size
                    if (cookCount < (numberOfCrew + 4) / 5) // Allows for 1 cook per 5 crew members
                    {
                        Title = "Cook Member";
                        Rank = 4;
                        cookCount++; // Increment cook count
                    }
                    break;
            }
        }

        public void ChangeStatus(string newStatus)
        {
            Status = newStatus;
        }

        public void SetWorkLocation(int roomNumber)
        {
            WorkLocation = roomNumber;
        }

        public void DecreaseHunger()
        {
            Hunger += HungerDecreaser;
        }

        public void DecreaseSleep()
        {
            SleepLevel -= SleepDecreaser;
            if (SleepLevel < 0) SleepLevel = 0; // Avoid negative sleep level
        }

        public void Eat()
        {
            Hunger -= 20;
            if (Hunger < 0) Hunger = 0;
        }

        public void Sleep()
        {
            SleepLevel += 10; // Assume each sleep increases sleep level
            if (SleepLevel > 100) SleepLevel = 100; // Max sleep level
        }

        public void DisplayUserInfo()
        {
            Console.WriteLine($"{Name,-20} {LastName,-20} {Title,-15} {Health,5} {Stamina,7} {Hunger,7} {SleepLevel,12} {CurrentLocation,15} {Status,13}");
        }
    }
}
