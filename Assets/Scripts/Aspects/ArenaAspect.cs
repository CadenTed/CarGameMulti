using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct ArenaAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO< LocalTransform > _transform;
        private LocalTransform Transform => _transform.ValueRO;

        private readonly RefRO< ArenaProperties > _arenaProperties;
        private readonly RefRW< SpawnRandom > _spawnRandom;
        private readonly RefRW< ZombieSpawnPoints > _zombieSpawnPoints;
        private readonly RefRW< ZombieSpawnTimer > _zombieSpawnTimer;

        public int NumberPointsToSpawn => _arenaProperties.ValueRO.NumberPointsToSpawn;
        public Entity SpawnPointPrefab => _arenaProperties.ValueRO.SpawnPointPrefab;

        public bool ZombieSpawnPointInitialized()
        {
            return _zombieSpawnPoints.ValueRO.Value.IsCreated && ZombieSpawnPointCount > 0;
        }

        private int ZombieSpawnPointCount => _zombieSpawnPoints.ValueRO.Value.Value.Value.Length;

        public LocalTransform GetRandomSpawnPointTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale( 0.5f )
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            randomPosition = _spawnRandom.ValueRW.Value.NextFloat3( MinCorner, MaxCorner );
            return randomPosition;
        }

        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;

        private float3 HalfDimensions => new()
        {
            x = _arenaProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z = _arenaProperties.ValueRO.FieldDimensions.y * 0.5f
        };

        private quaternion GetRandomRotation() =>
            quaternion.RotateY( _spawnRandom.ValueRW.Value.NextFloat( -0.25f, 0.25f ) );

        private float GetRandomScale(float min) => _spawnRandom.ValueRW.Value.NextFloat( min, 1f );

        public float2 GetRandomOffset()
        {
            return _spawnRandom.ValueRW.Value.NextFloat2();
        }

        public float ZombieSpawnTimer
        {
            get => _zombieSpawnTimer.ValueRO.Value;
            set => _zombieSpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnZombie => ZombieSpawnTimer <= 0f;

        public float ZombieSpawnRate => _arenaProperties.ValueRO.ZombieSpawnRate;

        public Entity ZombiePrefab => _arenaProperties.ValueRO.ZombPrefab;

        public LocalTransform GetZombieSpawnPoint()
        {
            var position = GetRandomZombieSpawnPoint();
            return new LocalTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(MathHelpers.GetHeading(position, Transform.Position)),
                Scale = 1f
            };
        }

        private float3 GetRandomZombieSpawnPoint()
        {
            return GetZombieSpawnPoint(_spawnRandom.ValueRW.Value.NextInt(ZombieSpawnPointCount));
        }

        private float3 GetZombieSpawnPoint(int i) => _zombieSpawnPoints.ValueRO.Value.Value.Value[i];

        public float3 Position => Transform.Position;

    }
}