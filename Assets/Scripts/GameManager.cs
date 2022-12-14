using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("Configurando");
        PhotonNetwork.ConnectUsingSettings();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    override
    public void OnConnectedToMaster()
    {
        print("conectado al Master");
        ButtonConnect();
    }

    public void ButtonConnect()
    {
        Debug.Log(PhotonNetwork.CountOfRooms);
        RoomOptions options = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }

    override
    public void OnJoinedRoom()
    {
        Debug.Log("conectada a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log(PhotonNetwork.CurrentRoom.Players);
        if (PhotonNetwork.IsMasterClient) 
        PhotonNetwork.InstantiateRoomObject("Player", new Vector3(-6.784447f, -1.89f, -0.1105546f), Quaternion.identity);
        else PhotonNetwork.InstantiateRoomObject("Player", new Vector3(-6.784447f, -1.89f, -0.1105546f), Quaternion.identity);


    }
}
