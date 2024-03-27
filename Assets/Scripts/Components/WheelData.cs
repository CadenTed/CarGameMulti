using Unity.Entities;

namespace Components
{
    public struct WheelData : IComponentData
    {
        public float MotorTorque;
        public float BrakeTorque;
        public float SteerAngle;
    }
}
