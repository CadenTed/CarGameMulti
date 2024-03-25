using Aspects;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnSpawnPointSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate( ref SystemState state )
        {
            state.RequireForUpdate<ArenaProperties>();
        }

        [BurstCompile]
        public void OnUpdate( ref SystemState state )
        {
            state.Enabled = false;
            var arenaEntity = SystemAPI.GetSingletonEntity< ArenaProperties >();
            var arena = SystemAPI.GetAspect< ArenaAspect >( arenaEntity );

            var ecb = new EntityCommandBuffer( Allocator.Temp );
            var spawnPointOffset = new float3( 0f, -2f, 1f );

            var builder = new BlobBuilder( Allocator.Temp );
            ref var spawnPoints = ref builder.ConstructRoot< ZombieSpawnPointsBlob >();
            var arrayBuilder = builder.Allocate( ref spawnPoints.Value, arena.NumberPointsToSpawn );

            for ( var i = 0; i < arena.NumberPointsToSpawn; i++ )
            {
                var newSpawnPoint = ecb.Instantiate( arena.SpawnPointPrefab );
                var newSpawnPointTransform = arena.GetRandomSpawnPointTransform();
                ecb.SetComponent(newSpawnPoint, newSpawnPointTransform);

                var newZombieSpawnPoint = newSpawnPointTransform.Position + spawnPointOffset;
                arrayBuilder[ i ] = newZombieSpawnPoint;
            }

            var blobAsset = builder.CreateBlobAssetReference< ZombieSpawnPointsBlob >( Allocator.Persistent );
            ecb.SetComponent(arenaEntity, new ZombieSpawnPoints{Value = blobAsset});
            builder.Dispose();
            
            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy( ref SystemState state )
        {

        }
    }
}