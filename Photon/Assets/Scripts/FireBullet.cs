using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBullet : MonoBehaviour
{
    private string prefabName = "bullet";
    private Vector3 spawnPosition; // The position where the prefab will be instantiated
    private Quaternion spawnRotation; // The rotation of the instantiated prefab
    GameObject prefab;
    PhotonView photonView;

    void Start()
    {
        prefab = Resources.Load<GameObject>(prefabName);
        photonView = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && photonView.IsMine)
            Spawn();
    }

    void Spawn()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        spawnPosition.y += 1.2f;
        spawnPosition += transform.forward / 3;
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogError($"Prefab with name {prefabName} not found in Resources.");
        }
    }
}
