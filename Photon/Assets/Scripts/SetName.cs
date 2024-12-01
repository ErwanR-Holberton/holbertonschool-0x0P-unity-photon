using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetName : MonoBehaviourPun
{
    public Text name;

    void Start()
    {
        if (photonView.IsMine)
            name.text = PlayerPrefs.GetString("Username");
        else
            photonView.RPC("RequestName", photonView.Owner, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    [PunRPC]
    public void RequestName(int requestingPlayerId)
    {
        if (photonView.IsMine)
            photonView.RPC("SendName", PhotonNetwork.CurrentRoom.GetPlayer(requestingPlayerId), PlayerPrefs.GetString("Username"));
    }

    [PunRPC]
    public void SendName(string Name)
    {
        name.text = Name;
    }
}
