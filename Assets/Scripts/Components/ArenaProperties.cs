using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public struct ArenaProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberPointsToSpawn;
        public Entity SpawnPointPrefab;
        public Entity ZombPrefab;
        public float ZombieSpawnRate;
    }

    public struct ZombieSpawnTimer : IComponentData
    {
        public float Value;
    }
}