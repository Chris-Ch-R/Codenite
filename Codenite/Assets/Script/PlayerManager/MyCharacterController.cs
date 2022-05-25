using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyCharacterController : MonoBehaviour
{
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


    private void Start()
    {
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

    [PunRPC]
    void getItem(string playername, string itemName)
    {
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
}
