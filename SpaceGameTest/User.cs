using System;
using System.Collections.Generic;

namespace SpaceGameUser
{
    public class User
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; } = "Crew Member"; // Default value
        public List<string> Professions { get; private set; }
        public int Rank { get; private set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
        public int Hunger { get; set; }
        public int SleepLevel { get; set; }
        public int CurrentLocation { get; set; }
        public int WorkLocation { get; set; }
        public string Status { get; private set; }

        public User(string name, string lastName, int rank, int roomNumber)
        {
            Name = name;
            LastName = lastName;
            Rank = rank;
            CurrentLocation = roomNumber; // Start in their assigned room
            Health = 100;
            Stamina = 100;
            Hunger = 0; // Start with no hunger
            SleepLevel = 100; // Start well rested
            Status = "Working"; // Default status
        }

        public void DisplayUserInfo()
        {
            Console.WriteLine($"{Name,-20} {LastName,-20} {Title,-15} {Health,5} {Stamina,7} {Hunger,7} {SleepLevel,12} {CurrentLocation,15} {Status,13}");
        }

        public void SetWorkLocation(int roomNumber)
        {
            WorkLocation = roomNumber;
        }

        public void ChangeStatus(string status)
        {
            Status = status;
        }

        public void Eat()
        {
            Hunger = Math.Max(0, Hunger - 10);
        }

        public void Sleep()
        {
            SleepLevel = Math.Min(100, SleepLevel + 20);
        }

        public void DecreaseHunger()
        {
            Hunger = Math.Min(100, Hunger + 5);
        }

        public void DecreaseSleep()
        {
            SleepLevel = Math.Max(0, SleepLevel - 10);
        }
    }
}
