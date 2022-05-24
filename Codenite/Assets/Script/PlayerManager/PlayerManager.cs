using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    [Header("Camera Manager")]
    public Camera mainCamera;
    private float zoomFactor = 3f;
    public float zoomLerpSpeed = 10;
    public float MaxZoom = 40f;
    public float MinZoom = 5f;
    public float CountDown = 10f;

    [Header("UI Manager")]
    public GameObject PlayerCamera;
    public GameObject MinimapCamera;
    public HealthBar HealthBar;
    public Text PlayerNameText;
    public Text countDownText;

    [Header("Object Manager")]
    public MyCharacterController Player;
    PhotonView view;
    Vector2 movement;
    Vector2 mousePos;
    bool IsGameStart = true;
    float currentZoom;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        if(view.IsMine)
        {
            mainCamera.GetComponent<CameraFollow>().SetOffset(new Vector3(0,0,-2));
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
        if(IsGameStart)
        {
            
            Invoke("ZoomIn", CountDown);
            if(CountDown > 0)
            {
                CountDown -= Time.deltaTime;
                updateTime(CountDown);
            }
            return;
        }
        else
        {
            countDownText.gameObject.SetActive(false);
        }
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
            if(Input.GetButtonDown("Fire2"))
            {
                Player.smiling();
            }
        }
        HealthBar.SetHealth(Player.currentHealth);
    }

    private void LateUpdate() {
        if(Player.IsDead() && view.IsMine)
        {
            Debug.Log("i'm DEAD!!!");
            gameObject.SetActive(false);
            DisconnectRoom();
        }    
    }

    public void DisconnectRoom()
    {
        Debug.Log("in DisconnectRoom Func");
    }

    private void ZoomIn()
    {
        currentZoom --;
        currentZoom = Mathf.Clamp(currentZoom, MinZoom, MaxZoom);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, currentZoom, Time.deltaTime * zoomLerpSpeed);
        IsGameStart = false;

        mainCamera.GetComponent<CameraFollow>().SetOffset(new Vector3(0,0,-1));
    }

    private void updateTime(float currentTime)
    {
        currentTime += 1;
        countDownText.text = Mathf.FloorToInt(currentTime % 60) + "";
        Debug.Log(currentTime);
    }
}
