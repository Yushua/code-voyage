using System;
using System.Collections.Generic;

namespace SpaceGameSystems
{
    public class SpaceSystem
    {
        private string name;
        private int xCoordinate;
        private int yCoordinate;
        private List<Planet> planets;
        private static Random random = new Random();

        public SpaceSystem(string systemName, int mapWidth, int mapHeight)
        {
            name = systemName;
            xCoordinate = random.Next(0, mapWidth);
            yCoordinate = random.Next(0, mapHeight);
            planets = new List<Planet>();

            int planetCount = random.Next(1, 11); // Between 1 and 10 planets
            for (int i = 0; i < planetCount; i++)
            {
                planets.Add(new Planet($"Planet-{i + 1}"));
            }
        }

        public void DisplaySystemInfo()
        {
            Console.WriteLine($"System: {name}, Location: ({xCoordinate}, {yCoordinate})");
            Console.WriteLine("Planets in this system:");
            foreach (var planet in planets)
            {
                planet.DisplayPlanetInfo();
            }
        }
    }

    public class Planet
    {
        private string name;
        private int size;
        private string type;
        private static Random random = new Random();

        public Planet(string planetName)
        {
            name = planetName;
            size = random.Next(1, 101);
            type = GenerateRandomType();
        }

        private string GenerateRandomType()
        {
            string[] types = { "Water", "Ground", "Hot", "Cold" };
            return types[random.Next(types.Length)];
        }

        public void DisplayPlanetInfo()
        {
            Console.WriteLine($"Planet: {this.name}, Size: {this.size}, Type: {this.type}");
        }
    }

    public class SpaceGame
    {
        public List<SpaceSystem> CreateSystems(int systemSize, int mapWidth, int mapHeight)
        {
            List<SpaceSystem> systems = new List<SpaceSystem>();

            for (int i = 0; i < systemSize; i++)
            {
                string systemName = $"System-{i + 1}";
                SpaceSystem newSystem = new SpaceSystem(systemName, mapWidth, mapHeight);
                systems.Add(newSystem);
            }

            return systems;
        }
    }
}
