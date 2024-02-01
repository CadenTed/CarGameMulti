using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Car Movement

public class CarController : NetworkBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    // Update wheel collider settings
    void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisual(axleInfo.leftWheel);
            ApplyLocalPositionToVisual(axleInfo.rightWheel);
        }
    }

    // Move visual wheels with colliders
    public void ApplyLocalPositionToVisual(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}

// Axle information
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor
    public bool steering; // does this wheel apply to steer angle
}
