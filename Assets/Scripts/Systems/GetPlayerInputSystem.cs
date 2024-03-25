// using Components;
// using Unity.Entities;
// using UnityEngine;
//
// namespace Systems
// {
//     [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
//     public partial class GetPlayerInputSystem : SystemBase
//     {
//         private MovementActions _movementActions;
//         private Entity _playerEntity;
//
//         protected override void OnCreate()
//         {
//             _movementActions = new MovementActions();
//         }
//
//         protected override void OnStartRunning()
//         {
//             _movementActions.Enable();
//         }
//
//         protected override void OnStopRunning()
//         {
//             _movementActions.Disable();
//         }
//         
//         protected override void OnUpdate()
//         {
//             var curMoveInput = _movementActions.Movement.PlayerMovement.ReadValue< Vector2 >();
//             
//             SystemAPI.SetSingleton(new PlayerMoveInput {Value = curMoveInput});
//         }
//     }
// }