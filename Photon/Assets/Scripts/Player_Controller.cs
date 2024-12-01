using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player_Controller : MonoBehaviourPun
{
    public float speed = 1.0f; // Speed of the player movement
    public float jumpForce = 5.0f; // Force applied when the player jumps
    private bool isGrounded; // To check if the player is on the ground
    private Rigidbody rb; // Reference to the player's Rigidbody component
    private Animator animator;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();


        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float moveHorizontal = 0f;
            float moveVertical = 0f;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                moveVertical = 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                moveVertical = -1f;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                moveHorizontal = -1f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                moveHorizontal = 1f;

            animator.SetFloat("Forward", moveVertical);
            animator.SetFloat("Turn", moveHorizontal / 10);

            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            Vector3 desiredMoveDirection = cameraForward * moveVertical + cameraRight * moveHorizontal;

            if (desiredMoveDirection != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(cameraForward);


            if (isGrounded)
                if (Input.GetKeyDown(KeyCode.Space))
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            transform.position = transform.position + desiredMoveDirection * speed * Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f))
                if (!hit.collider.CompareTag("Player"))
                {
                    isGrounded = true;
                    animator.SetBool("OnGround", true);
                }
                else
                    animator.SetBool("OnGround", false);
            else
            {
                isGrounded = false;
                animator.SetBool("OnGround", false);
            }

            if (transform.position.y < -20)
                transform.position = new Vector3(0, 20, 0);

        }
    }


    private void RotatePlayer(float rotationAmount)
    {
        Vector3 currentEulerAngles = transform.eulerAngles;
        currentEulerAngles.y += rotationAmount;
        transform.eulerAngles = currentEulerAngles;
    }

}
