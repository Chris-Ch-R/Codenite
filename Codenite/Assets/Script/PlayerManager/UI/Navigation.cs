using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public Rigidbody2D rb;
    public string Tag;
    private GameObject target;
    // Update is called once per frame
    void Update()
    {
        target = FindClosestTarget(Tag);
        Rotation(target.transform.position);
    }

    public void Rotation(Vector2 target)
    {
        Vector2 lookDir = target - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 360f;
        rb.rotation = angle;
    }
    public GameObject FindClosestTarget(string tag)
    {
        float distanceToClosestTarget = Mathf.Infinity;
        GameObject closestTarget = null;
        GameObject[] allTarget = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in allTarget)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance < distanceToClosestTarget)
            {
                distanceToClosestTarget = distance;
                closestTarget = target;
            }
        }
        return closestTarget;
    }
}
