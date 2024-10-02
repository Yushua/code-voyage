using systemCreate;
using universeCreate;
using planetCreate;

namespace universeCreate
{
    public class Universe
    {
        private List<systemCreate.System> systems;

        public Universe()
        {
            systems = new List<systemCreate.System>();
        }

        public void AddSystem(systemCreate.System system)
        {
            systems.Add(system);
        }

        public List<systemCreate.System> GetSystems()
        {
            return new List<systemCreate.System>(systems);
        }
    }
}