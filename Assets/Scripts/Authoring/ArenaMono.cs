using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Authoring
{
    public class ArenaMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberPointsToSpawn;
        public GameObject SpawnPointPrefab;
        public uint RandomSeed;
        public GameObject ZombiePrefab;
        public float ZombieSpawnRate;
        
    }
    
    public class ArenaMonoBaker : Baker< ArenaMono >
    {
        public override void Bake( ArenaMono authoring )
        {
            var arenaEntity = GetEntity( TransformUsageFlags.Dynamic );

            AddComponent( arenaEntity, new ArenaProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberPointsToSpawn = authoring.NumberPointsToSpawn,
                SpawnPointPrefab = GetEntity( authoring.SpawnPointPrefab, TransformUsageFlags.Dynamic ),
                ZombPrefab = GetEntity( authoring.ZombiePrefab, TransformUsageFlags.Dynamic ),
                ZombieSpawnRate = authoring.ZombieSpawnRate
            } );
            
            AddComponent( arenaEntity, new SpawnRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<ZombieSpawnPoints>(arenaEntity);
            AddComponent<ZombieSpawnTimer>(arenaEntity);
        }
    }
}