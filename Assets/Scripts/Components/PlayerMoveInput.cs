using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }
}