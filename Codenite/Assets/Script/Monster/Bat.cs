using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bat : Monster
{
    [Header("Bullet")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    public override void chasing()
    {
        GameObject target = FindClosestTarget("Player");
        if (target)
        {
            Rotation(target.transform.position);

            // slow down
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position , firePoint.rotation);
    }
}
