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
    public GameObject monsterObject;
    PhotonView view;
    Vector2 spawnPosition;
    Vector2 movement;
    Vector2 targetPos;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        PlayerNameText.text = monster.monsterName;
        HealthBar.SetHealth(monster.currentHealth);
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (monster.IsChaseRange())
        {
            GameObject target = monster.FindClosestTarget("Player");
            if (target)
            {
                monster.MoveTo(target.transform.position);
                monster.Rotation(target.transform.position);
            }
        }
        else if (!monster.IsChaseRange() && !monster.IsinHome(spawnPosition, 0.2f))
        {
            monster.MoveTo(spawnPosition);
            monster.Rotation(spawnPosition);
        }

        HealthBar.SetHealth(monster.currentHealth);
    }

    private void FixedUpdate()
    {
        if (monster.Isdead() )
        {
            Destroy(gameObject);
        }
    }
}

