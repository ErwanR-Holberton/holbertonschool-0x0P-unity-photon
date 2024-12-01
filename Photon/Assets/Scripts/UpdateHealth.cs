using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class UpdateHealth : MonoBehaviourPun
{

    public float playerHealth;
    private float InitialHealth = 100f;
    private float MaximumHealth = 100f;

    public Image fillElement;
    public Text HealthText;

    void Start()
    {

        if (!photonView.IsMine)
            photonView.RPC("RequestHealth", photonView.Owner, PhotonNetwork.LocalPlayer.ActorNumber);
        else
            playerHealth = InitialHealth;

        SetHealth(InitialHealth);
    }

    public void RegisterDamagesToAll(float amount)
    {
        Debug.Log($"{gameObject.name} Called register damage");
        photonView.RPC("HealthUpdate", RpcTarget.All, amount);
    }

    [PunRPC]
    public void HealthUpdate(float amount)
    {
        Debug.Log($"{gameObject.name} RPC Update {amount}");
        TakeDamage(amount);
    }
    [PunRPC]
    public void RequestHealth(int requestingPlayerId)
    {
        Debug.Log($"{gameObject.name} RequestHealth {requestingPlayerId}");
        if (photonView.IsMine)
            photonView.RPC("SendHealth", PhotonNetwork.CurrentRoom.GetPlayer(requestingPlayerId), playerHealth);
    }
    [PunRPC]
    public void SendHealth(float health)
    {
        Debug.Log($"{gameObject.name} RPC Send {health}");
        SetHealth(health);
    }

    private void TakeDamage(float amount)
    {
        Debug.Log($"{gameObject.name} Take Damage {amount}");
        Update_Health(amount);
    }

    public void SetHealth(float value)
    {
        Debug.Log($"{gameObject.name} SetHealth {value}");
        playerHealth = value;
        fillElement.fillAmount = Mathf.Clamp01(playerHealth / MaximumHealth);
        HealthText.text = $"{value.ToString()}/{MaximumHealth}";
        if (playerHealth <= 0)
            if (photonView.IsMine)
            {
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Menu");
            }
            else
                Debug.Log("Died");

    }
    public void Update_Health(float amount)
    {
        Debug.Log($"{gameObject.name} Update_Health");
        SetHealth(playerHealth + amount);
    }
}
