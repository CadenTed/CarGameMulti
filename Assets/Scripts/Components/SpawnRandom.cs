using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct SpawnRandom : IComponentData
    {
        public Random Value;
    }
}
