using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bat : Monster
{
    [Header("Fire")]
    public float fireRate;
    public float fireRange = 4f;
    public float offset = 0.3f;

    [Header("Bullet")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float nextTime = 0f;

    public override void chasing()
    {
        GameObject target = FindClosestTarget("Player");
        if (target)
        {
            Rotation(target.transform.position);

            //in fireRange
            if(Vector2.Distance(target.transform.position, rb.position) - fireRange <= offset)
            {
                agent.isStopped = true;
                Shoot();
            }
            // out of Range
            else if(Vector2.Distance(target.transform.position, rb.position) - fireRange > offset)
            {
                agent.isStopped = false;
                // if near or far TODO;
                agent.SetDestination(target.transform.position);
            }
        }
    }

    public void Shoot()
    {
        if(Time.time > nextTime)
        {
            nextTime = Time.time + fireRate;
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position , firePoint.rotation);
        }
    }
}
