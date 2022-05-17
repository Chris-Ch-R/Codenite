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
    public int maxHealth = 50;
    public int currentHealth;
    public float chaseRadius;
    public LayerMask whatIsPlayer;

    [Header("Character weapon")]
    public Animator attackAnimator;

    private bool isInChaseRange;
    float nextUpdateTime;
    float timeBetweenUpdates = 1f;
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, whatIsPlayer);
    }

    public void MoveTo(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.Normalize();
        rb.MovePosition(rb.position + (Vector2)dir * moveSpeed * Time.fixedDeltaTime);
    }

    public void Rotation(Vector2 target)
    {
        Vector2 lookDir = target - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 360f;
        rb.rotation = angle;
    }

    public bool IsinHome(Vector2 target, float offset)
    {
        if(Vector3.Distance(target, rb.position) <= offset)
            return true;
        return false;
    }

    public bool IsChaseRange()
    {
        return isInChaseRange;
    }

    public GameObject FindClosestTarget(string tag)
    {
        float distanceToClosestTarget = Mathf.Infinity;
        GameObject closestTarget = null;
        GameObject[] allTarget = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject target in allTarget)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if(distance < chaseRadius && distance < distanceToClosestTarget)
            {
                distanceToClosestTarget = distance;
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    public bool Isdead(){
        if(currentHealth <= 0){
            return true;
        }
        return false;
    }

    [PunRPC]
    void TakeDamage(int damage)
    {
        if(Time.time >= nextUpdateTime){
            currentHealth -= damage;
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }
}
