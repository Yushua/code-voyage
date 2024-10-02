using System.Collections.Generic;
using SpaceGameUser;
using SpaceGameShip;
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
                Room userRoom = ship.GetRoom(roomIndex++);
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
    }

    
}
