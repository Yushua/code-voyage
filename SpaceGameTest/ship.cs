using System;
using System.Collections.Generic;
using SpaceGameRoom;

namespace SpaceGameShip
{
    public class Ship
    {
        public string Name { get; private set; }
        public List<Room> Rooms { get; private set; }
        public Room Canteen { get; private set; }
        public Room Kitchen { get; private set; }
        public Room HeadRoom { get; private set; }

        public Ship(string name, int crewSize)
        {
            Name = name;
            Rooms = new List<Room>();

            // Initialize rooms with three parameters
            HeadRoom = new Room("Head Room", 1, 1); // Define Head Room
            Kitchen = new Room("Kitchen", 2, 2); // Define Kitchen
            Canteen = new Room("Canteen", 3, 3); // Define Canteen

            Rooms.Add(HeadRoom);
            Rooms.Add(Kitchen);
            Rooms.Add(Canteen);

            for (int i = 3; i < crewSize + 3; i++)
            {
                Rooms.Add(new Room($"Crew Room {i - 2}", i, i)); // Adjusted for Room constructor
            }
        }

        public void PrintRoomLocations()
        {
            Console.WriteLine("Room Locations:");
            Console.WriteLine($"{"Canteen",-20}: {Canteen.RoomName} (Room Number: {Canteen.RoomNumber})");
            Console.WriteLine($"{"Head Room",-20}: {HeadRoom.RoomName} (Room Number: {HeadRoom.RoomNumber})");
            Console.WriteLine($"{"Kitchen",-20}: {Kitchen.RoomName} (Room Number: {Kitchen.RoomNumber})");
            Console.WriteLine(); // Just for spacing
        }
    }
}
