using System;
using System.Numerics;
using System.Collections.Generic;
using planetCreate;

namespace systemCreate
{
    public class StarSystem
    {
        private Vector2 position;
        private List<Planet> planets;
        private Random random;
        public string Name { get; private set; }

        public StarSystem(Vector2 position, string name)
        {
            this.position = position;
            this.Name = name;
            planets = new List<Planet>();
            random = new Random();
        }

        public int PositionX => (int)position.X;
        public int PositionY => (int)position.Y;

        public Vector2 GetPosition()
        {
            return position;
        }

        public List<Planet> GetPlanets()
        {
            return new List<Planet>(planets);
        }

        public int GetPlanetSize()
        {
            return planets.Count;
        }

        public void GeneratePlanets()
        {
            int planetCount = random.Next(0, 13);
            for (int i = 0; i < planetCount; i++)
            {
                Vector2 planetPosition;
                bool positionValid;
                do
                {
                    positionValid = true;
                    planetPosition = new Vector2(random.Next(0, 1000), random.Next(0, 1000));
                    foreach (var planet in planets)
                    {
                        if (Vector2.Distance(planet.GetPosition(), planetPosition) < 35)
                        {
                            positionValid = false;
                            break;
                        }
                    }
                }
                while (!positionValid);
                Planet newPlanet = new Planet($"Planet {i + 1}", planetPosition);
                planets.Add(newPlanet);
            }
            Vector2 sunPosition = new Vector2(500, 500);
            planets.Sort((p1, p2) => Vector2.Distance(p1.GetPosition(), sunPosition)
                                        .CompareTo(Vector2.Distance(p2.GetPosition(), sunPosition)));
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].SetName($"Planet {i + 1}");
                int moonCount = random.Next(0, 6);
                planets[i].GenerateMoons(moonCount, random);
            }
            foreach (var planet in planets)
            {
                planet.SetSize(random.Next(1, 100));
            }
        }
    }
}
