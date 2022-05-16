using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealth;
    public Rigidbody2D rb;
    public GameObject PlayerCamera;
    Vector2 movement;
    Vector2 mousePos;
    public Animator attackAnimator;
    public Transform firePoint;
    public GameObject bulletPrefab;
    PhotonView view;
    public Text playerNameText;
    public HealthBar healthBar;

    private void Start(){
        view = GetComponent<PhotonView>();
        if(view.IsMine){
            PlayerCamera.SetActive(true);
            playerNameText.text = PhotonNetwork.NickName;

            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        else{
            PlayerCamera.SetActive(false);
            playerNameText.text = view.Owner.NickName;

            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            
            mousePos = PlayerCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void FixedUpdate() {
        healthBar.SetHealth(currentHealth);
        if(view.IsMine){
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 360f;
            rb.rotation = angle;
        }
    }

    void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position , firePoint.rotation);
        attackAnimator.SetTrigger("IsAttack");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void ReduceHealth(int damage)
    {
        currentHealth -= damage;
    }
}
