using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct InputData : IComponentData
    {
        public float2 Movement;
        public bool Braking;
    }
}
