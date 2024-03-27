using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class WheelMono : MonoBehaviour
    {
        public float motorTorque = 1000f;
        public float breakTorque = 3000f;
        public float steerAngle = 30f;

        private class WheelMonoBaker : Baker<WheelMono>
        {
            public override void Bake(WheelMono authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<WheelTag>(entity);
                AddComponent<InputData>(entity);
                AddComponent(entity, new WheelData
                {
                    MotorTorque = authoring.motorTorque,
                    BrakeTorque = authoring.breakTorque,
                    SteerAngle = authoring.steerAngle
                });
            }
        }
    }
}
