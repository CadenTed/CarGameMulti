using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class ZombieMono : MonoBehaviour
    {
        public float RiseRate;
        public float walkSpeed;
        public float walkAmplitude;
        public float walkFrequency;
    }

    public class ZombieMonoBaker : Baker< ZombieMono >
        {
            
            public override void Bake( ZombieMono authoring )
            {
                var zombieEntity = GetEntity( TransformUsageFlags.Dynamic );

                AddComponent( zombieEntity, new ZombieRiseRate { Value = authoring.RiseRate } );
                AddComponent( zombieEntity, new ZombieWalkProperties
                {
                    WalkSpeed = authoring.walkSpeed,
                    WalkAmplitude = authoring.walkAmplitude,
                    WalkFrequency = authoring.walkFrequency
                } );
                
                AddComponent<ZombieTimer>(zombieEntity);
                AddComponent<ZombieHeading>(zombieEntity);
                AddComponent<NewZombieTag>(zombieEntity);
            }
        }
    
}