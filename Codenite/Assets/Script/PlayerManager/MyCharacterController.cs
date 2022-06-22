using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;

    private Inventory inventory ;
    public Rigidbody2D rb;
    public PhotonView view;

    [Header("Character Info")]
    public float moveSpeed = 5f;
    float currentSpeed;
    public int maxHealth = 100;
    public int currentHealth;
    public int smileMaxValue = 5;
    public int smileCurrentValue;

    public float Maxcharge = 5f;

    [Header("Character weapon")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator attackAnimator;

    [Header("Character ui")]
    public Image pleaseWait;
    public SmileBar smileBar;

    private List<Item> itemList;

    private Vector2 startposition;
    private void Start()
    {
        startposition = transform.position;
        itemList = new List<Item>();
        initItemList();
        inventory = Inventory.Instance;
        rb = this.GetComponent<Rigidbody2D>(); // this line error
        currentHealth = maxHealth;
        currentSpeed = moveSpeed;
        smileCurrentValue = 0;

        view = GetComponent<PhotonView>();
    }

    public void Movement(Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    public void Rotation(Vector2 mousePos)
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 360f;
        rb.rotation = angle;
    }

    public void Shoot(float holdingTime)
    {
        if(holdingTime >= Maxcharge)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position , firePoint.rotation);
            bullet.GetComponent<Bullet>().SetOwnerViewID(view.ViewID);
            attackAnimator.SetTrigger("IsAttack");
        }
        smileBar.SetValue(0);
    }

    public bool IsDead()
    {
        if(currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void ResetSpeed()
    {
        currentSpeed = moveSpeed;
    }

    public void respawn()
    {
        transform.position = startposition;
        currentHealth = maxHealth;
        currentSpeed = moveSpeed;
    }

    [PunRPC]
    void getItem(string playername, string itemName)
    {
        foreach(Item item  in itemList){
            if (itemName.Equals(item.value))
            {
                inventory.AddItem(item); 
                break;       
            }

        }
        
        uiInventory.SetInventory(inventory);

        Debug.Log(playername + " Get item : " + itemName);
    }
    
    [PunRPC]
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }


    //UI setting

    public void SetplaseWait(bool active)
    {
        pleaseWait.gameObject.SetActive(active);
    }

    public void smiling(float currentTime)
    {
        int smileGauge = Mathf.FloorToInt(currentTime);
        if(smileGauge <= smileMaxValue)
            smileBar.SetValue(currentTime);
    }

    private void initItemList(){
        //TODO replace space
        itemList.Add(new Item{ itemType = Item.ItemType.FRAGMENT1, value = "Count <= 1"} );
        itemList.Add(new Item{ itemType = Item.ItemType.FRAGMENT2, value = "Count = 1"} );
        itemList.Add(new Item{ itemType = Item.ItemType.FRAGMENT3, value = "Count ++"} );
        itemList.Add(new Item{ itemType = Item.ItemType.FRAGMENT4, value = "Count >= 1"} );
        itemList.Add(new Item{ itemType = Item.ItemType.FRAGMENT5, value = "Count <= 10"} );
        itemList.Add(new Item{ itemType = Item.ItemType.FRAGMENT6, value = "Count = 0"} );
        
    }
}
