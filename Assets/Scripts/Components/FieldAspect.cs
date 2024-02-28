using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DefaultNamespace
{
    public readonly partial struct FieldAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transformAspect;

        private readonly RefRO<ArenaProperties> _arenaProperties;
        private readonly RefRW<SpawnRandom> _spawnRandom;
        private readonly RefRW<ZombieSpawnPoints> _zombieSpawnPoints;

        public NativeArray<float3> ZombieSpawnPoints
        {
            get => _zombieSpawnPoints.ValueRO.value;
            set => _zombieSpawnPoints.ValueRW.value = value;
        }
        
        

    }
}