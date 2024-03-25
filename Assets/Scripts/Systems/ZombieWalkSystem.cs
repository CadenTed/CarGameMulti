using System.Globalization;
using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(ZombieRiseSystem))]
    public partial struct ZombieWalkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate( ref SystemState state )
        {
        }

        [BurstCompile]
        public void OnUpdate( ref SystemState state )
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton< EndSimulationEntityCommandBufferSystem.Singleton >();

            new ZombieWalkJob
            {
                DeltaTime = deltaTime,
                // ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy( ref SystemState state )
        {

        }
    }

    [BurstCompile]
    public partial struct ZombieWalkJob : IJobEntity
    {
        public float DeltaTime;
        // public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute( ZombieWalkAspect zombie, [ChunkIndexInQuery]int sortKey )
        {
            zombie.Walk(DeltaTime);

        }
    }
}