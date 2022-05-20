using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieHand : MonoBehaviour
{
    public int damage;
    PhotonView view;
    // Update is called once per frame
    private void Start() {
        view = GetComponentsInParent<PhotonView>()[0];
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!view)
        {
            return;
        }
        if(view.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                PhotonView target = collision.gameObject.GetComponent<PhotonView>();
                target.RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
