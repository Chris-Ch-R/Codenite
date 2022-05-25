using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Laser : MonoBehaviour
{
    public float moveSpeed = 4f;
    public int bulletDamage;
    public float destroyTime = 2f;
    public PhotonView view;

    private void Start() {
        view = GetComponent<PhotonView>();
    }

    void Update(){
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(view.IsMine)
        {
            if(other.gameObject.tag == "Player")
            {
                PhotonView target = other.gameObject.GetComponent<PhotonView>();
                target.RPC("TakeDamage", RpcTarget.All, bulletDamage);
            }
        }
        Destroy(gameObject);
    }
}