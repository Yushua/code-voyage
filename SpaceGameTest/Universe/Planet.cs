using System.Numerics;
using System.Collections.Generic;

namespace planetCreate
{
    public class Planet
    {
        private string name;
        private Vector2 position;
        private List<Moon> moons;
        private int size;

        public Planet(string name, Vector2 position)
        {
            this.name = name;
            this.position = position;
            moons = new List<Moon>();
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetSize(int size)
        {
            this.size = size;
        }

        public void GenerateMoons(int moonCount, Random random)
        {
            for (int i = 0; i < moonCount; i++)
            {
                string moonName = $"{name} Moon {i + 1}";
                Vector2 moonPosition = new Vector2(random.Next(0, 50), random.Next(0, 50));
                moons.Add(new Moon(moonName, moonPosition));
            }
        }
    }

    public class Moon
    {
        private string name;
        private Vector2 position;

        public Moon(string name, Vector2 position)
        {
            this.name = name;
            this.position = position;
        }
    }
}
