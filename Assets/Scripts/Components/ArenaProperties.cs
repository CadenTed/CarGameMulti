using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public struct ArenaProperties : IComponentData
    {
        public float2 fieldDimensions;
        public Entity zombPrefab;
    }
}