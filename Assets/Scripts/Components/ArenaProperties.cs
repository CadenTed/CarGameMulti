using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ArenaProperties : IComponentData
    {
        public float2 Dimensions;
        public Entity ZombiePrefab;
        public float ZombieSpawnRate;
    }

    public struct Timer : IComponentData
    {
        public float Value;
    }
}
