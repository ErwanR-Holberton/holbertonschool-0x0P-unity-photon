using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Network : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Camera cam;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room.");
        GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        int randomNumber = Random.Range(1000, 9999); // Example: Generate a random 4-digit number
        playerInstance.name = $"player_{randomNumber}";
        FollowCameraRotation followCamera = playerInstance.GetComponentInChildren<FollowCameraRotation>();
        if (followCamera != null)
            followCamera.target = Camera.main.transform;
    }
}
