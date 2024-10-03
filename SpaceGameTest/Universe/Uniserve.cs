using System.Collections.Generic;
using System.Numerics;

namespace universeCreate
{
    public class Universe
    {
        private List<systemCreate.StarSystem> StarSystems;

        public Universe(int xSize, int ySize)
        {
            StarSystems = new List<systemCreate.StarSystem>();
            GenerateStarSystems(xSize, ySize);
        }

    private void GenerateStarSystems(int Width, int Height)
    {
        Random random = new Random();
        HashSet<(int, int)> existingCoordinates = new HashSet<(int, int)>();
        int n = 0;
        for (int x = 0; x < Width; x += 50)
        {
            for (int y = 0; y < Height; y += 50)
            {
                int systemsInChunk = random.Next(1, 21);

                for (int i = 0; i < systemsInChunk; i++)
                {
                    int systemX, systemY;

                    do
                    {
                        systemX = random.Next(x, x + 50);
                        systemY = random.Next(y, y + 50);
                    } while (existingCoordinates.Contains((systemX, systemY)));

                    existingCoordinates.Add((systemX, systemY));

                    var position = new Vector2(systemX, systemY);
                    systemCreate.StarSystem newSystem = new systemCreate.StarSystem(position, $"{n}Name {i}");
                    newSystem.GeneratePlanets();

                    StarSystems.Add(newSystem);
                }
                n++;
            }
        }
    }

        public void AddSystem(systemCreate.StarSystem system)
        {
            StarSystems.Add(system);
        }

        public List<systemCreate.StarSystem> GetStarSystems()
        {
            return new List<systemCreate.StarSystem>(StarSystems);
        }
    }
}
