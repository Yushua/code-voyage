using System.Collections.Generic;
using System.Numerics;

namespace universeCreate
{
    public class Universe
    {
        private List<systemCreate.StarSystem> StarSystems;

        public Universe(int xSize, int ySize, int sizeStarSystems)
        {
            StarSystems = new List<systemCreate.StarSystem>();
            GenerateStarSystems(xSize, ySize, sizeStarSystems);
        }

        private void GenerateStarSystems(int xSize, int ySize, int sizeStarSystems)
        {
            Random random = new Random();
            for (int i = 0; i < sizeStarSystems; i++)
            {
                Vector2 position = new Vector2(random.Next(0, xSize), random.Next(0, ySize));
                systemCreate.StarSystem newSystem = new systemCreate.StarSystem(position, $"Name {i}");
                newSystem.GeneratePlanets();
                StarSystems.Add(newSystem);
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
