using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct WheelRotateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new WheelRotateJob().Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

    [BurstCompile]
    public partial struct WheelRotateJob : IJobEntity
    {
        [BurstCompile]
        private void Execute(ref LocalTransform transform, in WheelData wheel, in InputData input)
        {
            // handle motor
            var motorTorque = wheel.MotorTorque * input.Movement.y;

            // handle braking
            var brakeTorque = wheel.BrakeTorque * (input.Braking ? 1 : 0);

            // handle steering
            var steerAngle = wheel.SteerAngle * input.Movement.x;
        }
    }
}
