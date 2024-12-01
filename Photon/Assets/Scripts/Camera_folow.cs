using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_folow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float rotationSpeed = 1.0f;
    private float yaw = 0.0f; // Horizontal rotation
    private float pitch = 0.0f; // Vertical rotation

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -89f, 89f); // Clamp the pitch to avoid flipping the camera

            Quaternion desiredRotation = Quaternion.Euler(pitch, yaw, 0.0f);

            Vector3 rotatedOffset = desiredRotation * offset;
            transform.position = player.position + rotatedOffset;
            transform.rotation = desiredRotation;
        }
    }

}


