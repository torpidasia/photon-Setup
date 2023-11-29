using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class playerController : MonoBehaviourPunCallbacks
{
    public static playerController Instance;
    public GameObject loadingScreen;
    public GameObject createRoomScreen;
    public GameObject createdRoomScreen;
    public InputField roomInput;
    public Text loadingText;
    public Text roomNameText;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        loadingScreen.SetActive(true);

        loadingText.text = "connecting to server...";
        PhotonNetwork.ConnectUsingSettings();
    }

    
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        loadingText.text = "joining lobby....";
    }
    public override void OnJoinedLobby()
    {
        loadingScreen.SetActive(false);
    }
    public void openCreateRoom()
    { createRoomScreen.SetActive(true);}
    public void createRoom()
    {
        if(!string.IsNullOrEmpty(roomInput.text))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            PhotonNetwork.CreateRoom(roomInput.text,roomOptions);
            loadingScreen.SetActive(true);
            loadingText.text = "creating room...";
        }
    }
    public override void OnCreatedRoom()
    {
        loadingScreen.SetActive(false );
        createdRoomScreen.SetActive(true);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

    }
    public void leaveRoom()
    {
        createdRoomScreen.SetActive(false);
        loadingScreen.SetActive(true);
        loadingText.text = "leaving Room....";
        PhotonNetwork.LeaveRoom();
    }
    public void OnApplicationQuit()
    {
        loadingScreen.SetActive(false ); }
    
}
