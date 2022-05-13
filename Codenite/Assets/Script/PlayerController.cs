using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    public Animator attackAnimator;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Vector2 lookDir = mousePos - rb.position;
        Vector2 lookDir =  rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 360f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position , firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

        attackAnimator.SetTrigger("IsAttack");
    }
}
