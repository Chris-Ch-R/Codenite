using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MonsterManager : MonoBehaviour
{
    [Header("UI Manager")]
    public HealthBar HealthBar;
    public Text PlayerNameText;

    [Header("Object Manager")]
    public MonsterController monster;
    PhotonView view;
    bool reset = false;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        PlayerNameText.text = monster.monsterName;
        HealthBar.SetHealth(monster.currentHealth);
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if(monster.IsInMoveRange() && monster.IsChase() && !monster.IsLowHealth() && !reset)
            {
                monster.chasing();
            }
            else if(monster.IsLowHealth() && monster.IsChase())
            {
                monster.retreat();
            }
            else if(monster.IsInHome(0.3f))
            {
                reset = false;
            }
            else
            {
                monster.goHome();
                reset = true;
            }
        }
        HealthBar.SetHealth(monster.currentHealth);
    }

    private void FixedUpdate()
    {
        if (monster.Isdead() && view.IsMine)
        {
            killSelf();
        }
    }

    private void killSelf()
    {
        gameObject.SetActive(false);
        Invoke("respawn", 5);
    }

    private void respawn()
    {
        Debug.Log("Respawn");
        monster.respawn();
        gameObject.SetActive(true);
    }
}

