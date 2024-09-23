using System;
using System.Collections.Generic;

namespace SpaceGameNamespace
{
    public class User
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
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
        public int LocationUser { get; set; }
        public string AgeInDays { get; set; }
        public int LocationSleep { get; set; }
        public int LocationWork { get; set; }

        // Static counters for unique titles
        private static int captainCount = 0;
        private static int headScientistCount = 0;
        private static int headEngineerCount = 0;
        private static int cookCount = 0;

        private static Random random = new Random();

        public User(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
            Professions = new List<string>();

            AssignTitleAndRank();

            Health = random.Next(50, 101);
            Stamina = random.Next(30, 100);
            PlaceOfOrigin = "Earth";
            SpaceNeeded = "Standard Cabin";
            Birthday = GenerateRandomBirthday();
            CombatStyle = "Unarmed";
            Speed = random.Next(1, 10);
            Happiness = random.Next(50, 100);
            Hunger = random.Next(0, 50);
            LocationUser = random.Next(0, 10);
            AgeInDays = "0";
            LocationSleep = 1;
            LocationWork = 2;
        }

        private void AssignTitleAndRank()
        {
            // Assign titles and ranks based on existing counts
            switch (true)
            {
                case bool _ when captainCount == 0:
                    Title = "Captain";
                    Rank = 1;
                    captainCount++;
                    break;

                case bool _ when headEngineerCount == 0:
                    Title = "Head Engineer";
                    Rank = 9;
                    Professions.Add("Engineer");
                    headEngineerCount++;
                    break;

                case bool _ when headScientistCount == 0:
                    Title = "Head Scientist";
                    Rank = 9;
                    Professions.Add("Scientist");
                    headScientistCount++;
                    break;

                case bool _ when cookCount == 0:
                    Title = "Cook";
                    Rank = random.Next(2, 10); // Random rank between 2 and 9
                    cookCount++;
                    break;

                default:
                    Title = "Crew Member"; // Default title
                    Rank = random.Next(2, 10); // Random rank
                    break;
            }

            // Add random professions if applicable
            if (Title != "Captain" && Title != "Head Engineer" && Title != "Head Scientist")
            {
                if (random.Next(0, 2) == 0) Professions.Add("Pilot");
                if (random.Next(0, 2) == 0) Professions.Add("Engineer");
                if (random.Next(0, 2) == 0) Professions.Add("Scientist");
            }
        }

        private string GenerateRandomBirthday()
        {
            int year = random.Next(2100, 2200);
            int month = random.Next(1, 13);
            int day = random.Next(1, 29);
            return $"Year {year}, Month {month}, Day {day}";
        }

        public void DisplayUserInfo()
        {
            Console.WriteLine($"Name: {Name} {LastName}");
            Console.WriteLine($"Title: {Title}, Rank: {Rank}");
            Console.WriteLine($"Professions: {string.Join(", ", Professions)}");
            // Console.WriteLine($"Health: {Health}, Stamina: {Stamina}");
            // Console.WriteLine($"Place of Origin: {PlaceOfOrigin}, Space Needed: {SpaceNeeded}");
            // Console.WriteLine($"Birthday: {Birthday}, Combat Style: {CombatStyle}");
            // Console.WriteLine($"Speed: {Speed}, Happiness: {Happiness}, Hunger: {Hunger}");
            // Console.WriteLine($"Location (User): {LocationUser}, Age in Days: {AgeInDays}");
            // Console.WriteLine($"Sleep Location: {LocationSleep}, Work Location: {LocationWork}");
            Console.WriteLine();
        }
    }
}
