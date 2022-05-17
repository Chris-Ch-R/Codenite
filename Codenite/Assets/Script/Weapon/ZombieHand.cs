using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieHand : MonoBehaviour
{
    public int damage;

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PhotonView target = collision.gameObject.GetComponent<PhotonView>();
            target.RPC("TakeDamage", RpcTarget.All, damage);
        }
    }
}
