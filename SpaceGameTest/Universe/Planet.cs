using System;
using System.Numerics;
using System.Collections.Generic;

namespace planetCreate
{
    public class Planet
    {
        private string name;
        private Vector2 position;
        private int size;
        private List<Moon> moons;

        public Planet(string name, Vector2 position)
        {
            this.name = name;
            this.position = position;
            moons = new List<Moon>();
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public int GetSize()
        {
            return size;
        }

        public void SetSize(int size)
        {
            this.size = size;
        }

        public List<Moon> GetMoons()
        {
            return new List<Moon>(moons);
        }

        public void GenerateMoons(int moonCount, Random random)
        {
            for (int i = 0; i < moonCount; i++)
            {
                string moonName = $"{name}-Moon{i + 1}";
                int moonSize = random.Next(1, 50);
                Moon moon = new Moon(moonName, moonSize);
                moons.Add(moon);
            }
        }
    }

    public class Moon
    {
        private string name;
        private int size;

        public Moon(string name, int size)
        {
            this.name = name;
            this.size = size;
        }

        public string GetName()
        {
            return name;
        }

        public int GetSize()
        {
            return size;
        }
    }
}
