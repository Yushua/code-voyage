using System.Collections.Generic;

namespace SpaceGameShip
{
    public class Ship
    {
        public string Name { get; private set; }
        public List<Room> Rooms { get; private set; }
        public Room Canteen { get; private set; }
        public Room Kitchen { get; private set; }
        public Room HeadRoom { get; private set; } // Add HeadRoom property

        public Ship(string name, int crewSize)
        {
            Name = name;
            Rooms = new List<Room>();

            // Assuming some logic to create rooms, including HeadRoom and Kitchen
            HeadRoom = new Room("Head Room", 1); // Define Head Room
            Kitchen = new Room("Kitchen", 2); // Define Kitchen
            Canteen = new Room("Canteen", 3); // Define Canteen

            // Add rooms to the list as needed
            Rooms.Add(HeadRoom);
            Rooms.Add(Kitchen);
            Rooms.Add(Canteen);

            // Add additional crew rooms as needed
            for (int i = 3; i < crewSize + 3; i++)
            {
                Rooms.Add(new Room($"Crew Room {i - 2}", i));
            }
        }

        public void PrintRoomLocations()
        {
            Console.WriteLine("Room Locations:");
            Console.WriteLine($"{"Canteen",-20}: {Canteen.RoomName} (Room Number: {Canteen.RoomNumber})");
            Console.WriteLine($"{"Head Room",-20}: {HeadRoom.RoomName} (Room Number: {HeadRoom.RoomNumber})");
            Console.WriteLine($"{"Kitchen",-20}: {Kitchen.RoomName} (Room Number: {Kitchen.RoomNumber})");
            // Add any other key rooms as necessary
            Console.WriteLine(); // Just for spacing
        }
    }

    public class Room
    {
        public string RoomName { get; }
        public int RoomNumber { get; }

        public Room(string roomName, int roomNumber)
        {
            RoomName = roomName;
            RoomNumber = roomNumber;
        }
    }
}
