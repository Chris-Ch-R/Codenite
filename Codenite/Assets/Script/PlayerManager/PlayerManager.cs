using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory ;
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

    void FixedUpdate() {
        if(view.IsMine){
    
        }
    }
}
