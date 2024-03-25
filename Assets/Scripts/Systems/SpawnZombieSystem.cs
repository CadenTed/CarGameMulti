using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate( ref SystemState state )
        {
            state.RequireForUpdate<ZombieSpawnTimer>();
        }

        [BurstCompile]
        public void OnUpdate( ref SystemState state )
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton< BeginInitializationEntityCommandBufferSystem.Singleton >();
            new SpawnZombieJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }

        [BurstCompile]
        public void OnDestroy( ref SystemState state )
        {

        }
    }
    
    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute( ArenaAspect arena )
        {
            arena.ZombieSpawnTimer -= DeltaTime;
            if ( !arena.TimeToSpawnZombie ) return;
            if ( !arena.ZombieSpawnPointInitialized() ) return;

            arena.ZombieSpawnTimer = arena.ZombieSpawnRate;
            var newZombie = ECB.Instantiate( arena.ZombiePrefab );

            var newZombieTransform = arena.GetZombieSpawnPoint();
            ECB.SetComponent(newZombie, newZombieTransform);

            var zombieHeading = MathHelpers.GetHeading( newZombieTransform.Position, arena.Position );
            ECB.SetComponent(newZombie, new ZombieHeading{Value = zombieHeading});

        }
    }
}