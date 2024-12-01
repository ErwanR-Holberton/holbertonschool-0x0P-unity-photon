using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class move : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    private Rigidbody rb;
    PhotonView photonView;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
            Camera.main.GetComponent<Camera_folow>().player = this.transform;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Get input for movement
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 move = (cameraForward * moveZ + cameraRight * moveX) * speed;

            Vector3 newPosition = rb.position + move * Time.deltaTime;
            rb.MovePosition(newPosition);

            if (move.magnitude > 0.1f)  // To avoid unwanted rotation when not moving
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);  // rotationSpeed controls the rotation speed
            }
        }
    }
}
