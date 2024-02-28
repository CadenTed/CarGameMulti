using DefaultNamespace;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Authoring
{
    public class SpawnAuthoring : MonoBehaviour
    {
        public float2 fieldDimensions;
        public GameObject zombPrefab;
        public uint randomSeed;
        private class SpawnAuthoringBaker : Baker<SpawnAuthoring>
        {
            public override void Bake(SpawnAuthoring authoring)
            {
                AddComponent(new ArenaProperties
                {
                    fieldDimensions = authoring.fieldDimensions,
                    zombPrefab = GetEntity(authoring.zombPrefab)
                });
                
                AddComponent(new SpawnRandom
                {
                    value = Random.CreateFromIndex(authoring.randomSeed)
                });
                
                AddComponent<ZombieSpawnPoints>();
            }
        }
    }
}