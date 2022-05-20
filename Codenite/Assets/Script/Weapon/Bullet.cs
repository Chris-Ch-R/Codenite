using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{    
    public float moveSpeed = 4f;
    public int bulletDamage;
    public float destroyTime = 2f;
    PhotonView view;
    private void Start() {
        view = GetComponent<PhotonView>();
    }
    void Update(){
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        Destroy(gameObject, destroyTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(view.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                PhotonView target = collision.gameObject.GetComponent<PhotonView>();
                if (target.IsMine){
                    target.RPC("TakeDamage", RpcTarget.All, bulletDamage);
                }
            }

            if (collision.gameObject.tag == "Monster")
            {
                PhotonView target = collision.gameObject.GetComponent<PhotonView>();
                target.RPC("TakeDamage", RpcTarget.All, bulletDamage);
            }
        }
        Destroy(gameObject);
    }
}
