using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 15f;
    public bool shouldTurn;

    Transform target;
    NavMeshAgent agent;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldTurn)
        {
            Vector3 lookVector = player.transform.position - transform.position;
            lookVector.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }
        Follow();
    }

    void Follow()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && distance > agent.stoppingDistance)
        {
            agent.SetDestination(target.position);
            lookRadius = 50;
            if (distance >= 15)
            {
                agent.speed = 8;
            }
            else
            {
                agent.speed = 3;
            }
        }

    }
   
    void OnTriggerEnter()
    {
        shouldTurn = true;
        agent.ResetPath();
        transform.LookAt(player.transform);
 
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
