using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Character weapon")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator attackAnimator;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); // this line error
        currentHealth = maxHealth;
        currentSpeed = moveSpeed;

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

    public void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position , firePoint.rotation);
        bullet.GetComponent<Bullet>().SetOwnerViewID(view.ViewID);
        attackAnimator.SetTrigger("IsAttack");
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
    void getItem(string playername)
    {
        Debug.Log(playername + " Get item");
    }
    
    [PunRPC]
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

}
