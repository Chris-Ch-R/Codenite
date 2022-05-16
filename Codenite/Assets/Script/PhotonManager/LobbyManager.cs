using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;
    public Text numberPlayerText;

    public RoomCell roomCellPrefab;
    List<RoomCell> roomItemsList = new List<RoomCell>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    List<PlayerCell> playerCells = new List<PlayerCell>();
    public PlayerCell PlayerCellPrefab;
    public Transform playerItemParent;

    public GameObject playButton;

    [Header("Room Option")]
    public int MaxPlayers = 1;
    public string Password;
    RoomOptions roomOptions;
    private void Start() {
        PhotonNetwork.JoinLobby();
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    private void Update(){
        if(PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true);

            playButton.GetComponent<Button>().interactable = false;
            if(PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                playButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void SetRoomOption(){
        roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)MaxPlayers;
    }

    public void SetMaxPlayer(int num_player)
    {
        MaxPlayers = num_player;
    }
    public void OnClickCreate()
    {
        if(roomInputField.text.Length >= 3){
            SetRoomOption();
            PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        numberPlayerText.text = PhotonNetwork.CurrentRoom.Players.Count + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach(RoomCell item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in roomList)
        {
            RoomCell newRoom = Instantiate(roomCellPrefab, contentObject);
            newRoom.SetRoomInfo(room.Name, room.PlayerCount, room.MaxPlayers);
            if(room.IsOpen == false)
            {
                newRoom.SetInteractable(false);
            }

            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName){
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach(PlayerCell cell in playerCells)
        {
            Destroy(cell.gameObject);
        }
        playerCells.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerCell newPlayerCell = Instantiate(PlayerCellPrefab, playerItemParent);
            newPlayerCell.SetPlayerInfo(player.Value);
            playerCells.Add(newPlayerCell);
        }
        numberPlayerText.text = PhotonNetwork.CurrentRoom.Players.Count + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void OnClickPlayButton(){
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel("Game");
    }
}
