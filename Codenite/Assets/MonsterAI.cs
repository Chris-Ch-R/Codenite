using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour
{
    GameObject target;
    public float chaseRadius = 4f;
    // Start is called before the first frame update
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestTarget("Player");
        if(target)
        {
            
            agent.SetDestination(target.transform.position);
        }
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
}
