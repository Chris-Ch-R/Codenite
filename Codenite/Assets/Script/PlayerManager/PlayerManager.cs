using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    [Header("UI Manager")]
    public GameObject PlayerCamera;
    public GameObject MinimapCamera;
    public HealthBar HealthBar;
    public Text PlayerNameText;

    [Header("Object Manager")]
    public MyCharacterController Player;
    PhotonView view;
    Vector2 movement;
    Vector2 mousePos;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        if(view.IsMine)
        {
            PlayerCamera.SetActive(true);
            MinimapCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerCamera.SetActive(false);
            MinimapCamera.SetActive(false);
            PlayerNameText.text = view.Owner.NickName;
        }

        HealthBar.SetHealth(Player.currentHealth);
    }

    private void Update()
    {
        if(view.IsMine){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            mousePos = PlayerCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            Player.Movement(movement);
            Player.Rotation(mousePos);
            if(Input.GetButtonDown("Fire1"))
            {
                Player.Shoot();
            }
        }
        HealthBar.SetHealth(Player.currentHealth);
    }

    private void LateUpdate() {
        if(Player.IsDead() && view.IsMine)
        {
            Debug.Log("i'm DEAD!!!");
            DisconnectRoom();
        }    
    }

    public void DisconnectRoom()
    {
        Debug.Log("in DisconnectRoom Func");
        // PhotonNetwork.LeaveRoom();
        // while(PhotonNetwork.InRoom)
        //     return ;
        // SceneManager.LoadScene("Lobby");
    }
}
