using UnityEngine;

// 

public class EffectObject : MonoBehaviour
{
    [Header("Unity Setup")]
    public float time = 5;
    void Start()
    {
        Destroy(gameObject, time);
    }

}
