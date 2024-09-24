using System.Collections.Generic;
using SpaceGameUser;
using SpaceGameShip;
using SpaceGameSystems;
using System.Windows.Forms;
using SpaceGameRoom;

namespace SpaceGame
{
    public class GameSetup
    {
        public List<User> CreateCharacters(int numberOfCrew, Ship ship)
        {
            List<User> users = new List<User>();
            int roomIndex = 0;

            for (int i = 0; i < numberOfCrew; i++)
            {
                string name = $"UserFirstName{i}";
                string lastName = $"UserLastName{i}";
                Room userRoom = ship.Rooms[roomIndex++];
                User newUser = new User(name, lastName, numberOfCrew, userRoom.RoomNumber);

                switch (newUser.Title)
                {
                    case "Captain":
                    case "Head Engineer":
                    case "Head Scientist":
                        newUser.SetWorkLocation(ship.HeadRoom.RoomNumber);
                        break;
                    case "Cook":
                        newUser.SetWorkLocation(ship.Kitchen.RoomNumber);
                        break;
                    default:
                        newUser.SetWorkLocation(userRoom.RoomNumber);
                        break;
                }

                users.Add(newUser);
            }

            return users;
        }

        public List<SpaceSystem> CreateSystems(int systemSize)
        {
            List<SpaceSystem> systems = new List<SpaceSystem>();

            for (int i = 0; i < systemSize; i++)
            {
                string systemName = $"System-{i + 1}";
                SpaceSystem newSystem = new SpaceSystem(systemName, 100, 100);
                systems.Add(newSystem);
            }

            return systems;
        }

        public void UpdateAndDisplayGameTime(ref int year, ref int month, ref int day, ref int hour, ref int minute, Label displayLabel)
        {
            minute += 10;

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
