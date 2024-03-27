using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class InputSystem : SystemBase
    {
        private GameControls _controls;

        protected override void OnCreate()
        {
            RequireForUpdate<InputData>();

            _controls = new GameControls();
            _controls.Enable();
        }

        protected override void OnUpdate()
        {
            foreach (var data in SystemAPI.Query<RefRW<InputData>>())
            {
                data.ValueRW.Movement = _controls.Player.Movement.ReadValue<Vector2>();
                data.ValueRW.Braking = _controls.Player.Braking.ReadValue<float>() > 0;
            }
        }

        protected override void OnDestroy()
        {
            _controls.Disable();
        }
    }
}
