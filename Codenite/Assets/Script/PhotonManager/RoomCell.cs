using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCell : MonoBehaviour
{
    public Text roomName;
    public Text NumPlayer;
    LobbyManager manager;

    private void Start() {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomInfo(string _roomName, int currentPlayer, int maxPlayer)
    {
        roomName.text = _roomName;
        NumPlayer.text = currentPlayer + "/" + maxPlayer;
    }

    public void SetInteractable(bool isInteractable)
    {
        this.gameObject.GetComponent<Button>().interactable = isInteractable;
    }

    public void OnClickCell()
    {
        manager.JoinRoom(roomName.text);
    }
}
