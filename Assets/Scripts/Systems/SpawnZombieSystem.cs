using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnZombieJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(ref SpawnRandom random, in ArenaProperties arena, ref Timer timer)
        {
            timer.Value -= DeltaTime;
            if (timer.Value > 0) return;

            timer.Value = random.Value.NextFloat(0, arena.ZombieSpawnRate);

            var zombie = ECB.Instantiate(arena.ZombiePrefab);
            ECB.SetComponent(zombie, LocalTransform.FromPosition(
                new float3(random.Value.NextFloat(-arena.Dimensions.x, arena.Dimensions.x), 0,
                    random.Value.NextFloat(-arena.Dimensions.y, arena.Dimensions.y))
            ));
        }
    }
}
