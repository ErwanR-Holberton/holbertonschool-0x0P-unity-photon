using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerHealth : MonoBehaviour
{
    private PhotonView photonView; // Declare the PhotonView component
    public Image healthBarFillImage; // Reference to the Image component for the health bar fill

    private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentHealth = maxHealth; // Initialize player health
        UpdateHealthBar();
    }
    void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetKey(KeyCode.G))
        {
            TakeDamage(5f);
            Debug.Log("Damage");
        }
    }

    [PunRPC]
    public void UpdateHealth(float healthPercentage)
    {
        SetHealthBarFill(healthPercentage);
    }

    public void SetHealth(float healthPercentage)
    {
        currentHealth = Mathf.Clamp(healthPercentage * maxHealth, 0, maxHealth);
        float healthPercentageNormalized = currentHealth / maxHealth;
        photonView.RPC("UpdateHealth", RpcTarget.All, healthPercentageNormalized);
    }

    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        SetHealthBarFill(healthPercentage);
    }

    void SetHealthBarFill(float percentage)
    {
        // Ensure percentage is between 0 and 1
        percentage = Mathf.Clamp01(percentage);
        healthBarFillImage.fillAmount = percentage; // Update the health bar fill amount
    }

    public void TakeDamage(float damageAmount)
    {
        if (!photonView.IsMine)
            return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        SetHealth(currentHealth / maxHealth);
    }
}
