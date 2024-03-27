using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Authoring
{
    public class ArenaMono : MonoBehaviour
    {
        public Vector2 dimensions = new(74, 74);

        public GameObject zombiePrefab;
        public float spawnRate = 1f;

        private class ArenaMonoBaker : Baker<ArenaMono>
        {
            public override void Bake(ArenaMono authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ArenaProperties
                {
                    Dimensions = authoring.dimensions,
                    ZombiePrefab = GetEntity(authoring.zombiePrefab, TransformUsageFlags.Dynamic),
                    ZombieSpawnRate = authoring.spawnRate,
                });
                AddComponent(entity, new Timer
                {
                    Value = UnityEngine.Random.Range(0, authoring.spawnRate)
                });
                AddComponent(entity, new SpawnRandom
                {
                    Value = Random.CreateFromIndex((uint)entity.Index)
                });
            }
        }
    }
}
