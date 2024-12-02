using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Network : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Camera cam;
    public List<Vector3> spawnPoints = new List<Vector3>();

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
        GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, GetAndRemoveRandomPoint(), Quaternion.identity);
        int randomNumber = Random.Range(1000, 9999); // Example: Generate a random 4-digit number
        playerInstance.name = $"player_{randomNumber}";
        FollowCameraRotation followCamera = playerInstance.GetComponentInChildren<FollowCameraRotation>();
        if (followCamera != null)
            followCamera.target = Camera.main.transform;
    }

    public Vector3 GetAndRemoveRandomPoint()
    {
        if (spawnPoints.Count == 0)
            Debug.LogWarning("No spawn points available!");

        int randomIndex = Random.Range(0, spawnPoints.Count); // Get a random index
        Vector3 selectedPoint = spawnPoints[randomIndex];     // Get the point at that index
        spawnPoints.RemoveAt(randomIndex);                   // Remove it from the list
        return selectedPoint;                                // Return the selected point
    }
}
