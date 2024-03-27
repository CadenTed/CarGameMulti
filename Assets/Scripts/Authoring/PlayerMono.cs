using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class PlayerMono : MonoBehaviour
    {
        private class PlayerMonoBaker : Baker<PlayerMono>
        {
            public override void Bake(PlayerMono authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
            }
        }
    }
}
