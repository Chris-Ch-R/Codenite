using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonsterController : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Character Info")]
    public string monsterName;
    public float moveSpeed = 2f;
    float currentSpeed;
    public int maxHealth = 50;
    public int currentHealth;
    public float chaseRadius;
    public float moveRadius;
    public LayerMask whatIsPlayer;

    [Header("Character weapon")]
    public Animator attackAnimator;
    private Vector2 spawnPosition;
    private void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = rb.transform.position;
    }

    public void MoveTo(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.Normalize();
        rb.MovePosition(rb.position + (Vector2)dir * currentSpeed * Time.fixedDeltaTime);
    }

    public void Rotation(Vector2 target)
    {
        Vector2 lookDir = target - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 360f;
        rb.rotation = angle;
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void ResetSpeed()
    {
        currentSpeed = moveSpeed;
    }

    public bool IsInMoveRange()
    {
        return Vector3.Distance(spawnPosition, rb.position) <= moveRadius;
    }

    public bool IsInHome(float offset)
    {
        return Vector3.Distance(spawnPosition, rb.position) <= offset;
    }

    public bool IsChase()
    {
        bool isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, whatIsPlayer);
        return isInChaseRange;
    }

    public bool IsLowHealth()
    {
        return currentHealth <= maxHealth / 3;
    }

    public bool Isdead()
    {
        return currentHealth <= 0;
    }

    public GameObject FindClosestTarget(string tag)
    {
        float distanceToClosestTarget = Mathf.Infinity;
        GameObject closestTarget = null;
        GameObject[] allTarget = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in allTarget)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance < chaseRadius && distance < distanceToClosestTarget)
            {
                distanceToClosestTarget = distance;
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    public void chasing()
    {
        GameObject target = FindClosestTarget("Player");
        if (target)
        {
            MoveTo(target.transform.position);
            Rotation(target.transform.position);
        }
    }

    public void goHome()
    {
        ResetSpeed();
        MoveTo(spawnPosition);
        Rotation(spawnPosition);
    }

    public void retreat()
    {
        GameObject objectTarget = FindClosestTarget("Player");
        if (objectTarget)
        {
            SetSpeed(7f);
            Vector3 target = objectTarget.transform.position;

            //move out
            Vector3 dir = target - transform.position;
            dir.Normalize();
            rb.MovePosition(rb.position + (Vector2)dir * (currentSpeed * -1) * Time.fixedDeltaTime);

            //rotate out
            Vector2 lookDir = (Vector2)target - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -180f;
            rb.rotation = angle;
        }
    }

    public void respawn()
    {
        currentHealth = maxHealth;
        currentSpeed = moveSpeed;
        rb.transform.position = spawnPosition;
    }

    [PunRPC]
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
