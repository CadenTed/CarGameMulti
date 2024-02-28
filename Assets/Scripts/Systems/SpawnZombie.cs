using DefaultNamespace;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnZombie : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var arenaEntity = SystemAPI.GetSingletonEntity<ArenaProperties>();
            var arena = SystemAPI.GetAspect<FieldAspect>(arenaEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            var spawnPoints = new NativeArray<float3>(Allocator.Temp);
            
            

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}