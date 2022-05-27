using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bat : Monster
{
    [Header("Fire")]
    public float fireRate;

    [Header("Bullet")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float nextTime;
    private float currentTime = 0;

    public override void chasing()
    {
        GameObject target = FindClosestTarget("Player");
        if (target)
        {
            Rotation(target.transform.position);
            // slow down
            if(currentTime >= nextTime)
            {
                nextTime = nextTime + fireRate;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position , firePoint.rotation);
    }
}
