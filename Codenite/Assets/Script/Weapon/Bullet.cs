using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{    
    public float moveSpeed = 4f;
    public int bulletDamage;
    public float destroyTime = 2f;
    public PhotonView view;
    public int OwnerView;
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
            if (collision.gameObject.tag == "Monster")
            {
                PhotonView target = collision.gameObject.GetComponent<PhotonView>();
                target.RPC("TakeDamage", RpcTarget.All, bulletDamage);

                //getItem
                if(collision.gameObject.GetComponent<MonsterController>().Isdead())
                {
                    PhotonView player = PhotonView.Find(OwnerView);
                    player.RPC("getItem", RpcTarget.All, view.Owner.NickName);
                }
            }
        }
        Destroy(gameObject);
    }

    public void SetOwnerViewID(int ID)
    {
        OwnerView = ID;
    }
}
