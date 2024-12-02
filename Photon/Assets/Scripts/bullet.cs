using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject owner;
    private float speed = 20f;
    private float lifetime = 5f;
    private int damage = 10;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("No Rigidbody found! Adding one dynamically.");
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; // Disable gravity for typical bullets
            rb.isKinematic = true; // Kinematic, as we're controlling the movement manually
        }

        Destroy(gameObject, lifetime); // Set the bullet to destroy itself after the lifetime
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Avoid self-collision or collisions with certain objects
        if (other.gameObject == this.gameObject) return;
        if (owner != null && (other.gameObject == owner || other.transform.IsChildOf(owner.transform)))
        {
            Debug.Log("Owner");
            return;
        }

        UpdateHealth health = other.GetComponent<UpdateHealth>();
        if (health != null)
        {
            health.RegisterDamagesToAll(- damage);
        }

        Destroy(gameObject);
    }

    private void Reset()
    {
        Collider childCollider = GetComponentInChildren<Collider>();
        if (childCollider == null)
            Debug.LogError("No child collider found! Add a child object with a Collider component.");
        else
            if (!childCollider.isTrigger)
            {
                Debug.LogWarning("Child collider is not set as Trigger. Changing to Trigger for proper collision handling.");
                childCollider.isTrigger = true;
            }
    }
}
