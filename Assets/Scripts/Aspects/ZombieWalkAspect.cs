using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct ZombieWalkAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<ZombieTimer> _walkTimer;
        private readonly RefRO<ZombieWalkProperties> _walkProperties;
        private readonly RefRO<ZombieHeading> _heading;

        private float WalkSpeed => _walkProperties.ValueRO.WalkSpeed;
        private float WalkAmplitude => _walkProperties.ValueRO.WalkAmplitude;
        private float WalkFrequency => _walkProperties.ValueRO.WalkFrequency;
        private float Heading => _heading.ValueRO.Value;

        private float WalkTimer
        {
            get => _walkTimer.ValueRO.Value;
            set => _walkTimer.ValueRW.Value = value;
        }

        public void Walk(float deltaTime)
        {
            WalkTimer += deltaTime;

            var current = _transform.ValueRO.Position;
            var target = new float3(0, 0, 0);
            var maxDistanceDelta = WalkSpeed * deltaTime;

            var x = target.x - current.x;
            var y = target.y - current.y;
            var z = target.z - current.z;

            var dist = x * x + y * y + z * z;
            if (dist == 0 || dist <= maxDistanceDelta * maxDistanceDelta)
            {
                _transform.ValueRW.Position = target;
            }
            else
            {
                var magnitude = math.sqrt(dist);

                _transform.ValueRW.Position = new float3(current.x + x / magnitude * maxDistanceDelta,
                    current.y + y / magnitude * maxDistanceDelta,
                    current.z + z / magnitude * maxDistanceDelta);
            }

            var swayAngle = WalkAmplitude * math.sin(WalkFrequency * WalkTimer);
            _transform.ValueRW.Rotation = quaternion.Euler(0, Heading, swayAngle);
        }

        // public bool IsInStoppingRange( float3 brainPosition, float brainRadiusSq )
        // {
        //     return math.distancesq( brainPosition, _transform.ValueRO.Position ) <= brainRadiusSq;
        // }
    }
}
