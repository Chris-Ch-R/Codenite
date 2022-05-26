using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory ;
    [Header("Camera Manager")]
    public Camera mainCamera;
    public float zoomLerpSpeed = 10;
    public float MaxZoom = 40f;
    public float MinZoom = 5f;
    public float CountDown = 10f;

    [Header("UI Manager")]
    public GameObject PlayerCamera;
    public GameObject MinimapCamera;
    public GameObject WorldMap;
    public HealthBar HealthBar;
    public Text PlayerNameText;
    public Text countDownText;

    [Header("Object Manager")]
    public MyCharacterController Player;
    PhotonView view;
    Vector2 movement;
    Vector2 mousePos;
    float holdDownStartTime;
    bool IsGameStart = true;
    float currentZoom;
    private void Start()
    {
        inventory = Inventory.Instance;
        Item item1 = new Item {id =  0, itemType = Item.ItemType.Common , value = "count=1" };
        Item item2 = new Item {id =  1, itemType =Item.ItemType.Rare , value = "count++" };
        Item item3 = new Item {id =  2, itemType =Item.ItemType.Epic , value = "count<=10" };
        // inventory.AddItem(item1);
        inventory.AddItem(item1);
        inventory.AddItem(item2);
        inventory.AddItem(item3);
        // inventory.AddItem(item5);
        uiInventory.SetInventory(inventory);
        view = GetComponent<PhotonView>();
        if(view.IsMine)
        {
            mainCamera.GetComponent<CameraFollow>().SetOffset(new Vector3(0,0,-2));
            PlayerCamera.SetActive(true);
            MinimapCamera.SetActive(true);
            WorldMap.SetActive(false);
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
            else
                IsGameStart = false;
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

            //Change and Shoot
            if(Input.GetButtonDown("Fire1"))
            {
                //start holding
                holdDownStartTime = Time.time;
            }

            if(Input.GetButton("Fire1"))
            {
                // still down
                float holdDownTime = Time.time - holdDownStartTime;
                Player.smiling(holdDownTime);
            }

            if(Input.GetButtonUp("Fire1"))
            {
                // release, Launch!!
                float holdDownTime = Time.time - holdDownStartTime;
                Player.Shoot(holdDownTime);
            }



            if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab))
            {
                WorldMap.SetActive(true);
            }
            if(Input.GetKeyUp(KeyCode.M) || Input.GetKeyUp(KeyCode.Tab))
            {
                WorldMap.SetActive(false);
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
        mainCamera.GetComponent<CameraFollow>().SetOffset(new Vector3(0,0,-1));
    }

    private void updateTime(float currentTime)
    {
        currentTime += 1;
        countDownText.text = Mathf.FloorToInt(currentTime % 60) + "";
    }
}
