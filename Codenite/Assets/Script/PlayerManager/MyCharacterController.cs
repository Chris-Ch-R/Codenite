using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyCharacterController : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Character Info")]
    public float moveSpeed = 5f;
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
    }

    public void Movement(Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
        attackAnimator.SetTrigger("IsAttack");
    }

    
    [PunRPC]
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
