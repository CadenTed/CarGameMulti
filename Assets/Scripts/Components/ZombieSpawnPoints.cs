using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct ZombieSpawnPoints : IComponentData
    {
        public BlobAssetReference<ZombieSpawnPointsBlob> Value;
    }

    public struct ZombieSpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}