using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    enum Estados
    {
        Chase,
        Patrolling,
        Attacking
    };
    [SerializeField]
    private Transform objective;
    [SerializeField]
    private Transform[] wayPoints;
    private NavMeshAgent agent;
    [SerializeField]
    private float distanceAttack;
    private Estados curEst;
    private int wpIndex=0;
    private Transform curWP;
    [SerializeField]
    private Health playerhealth;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float coolDown;
    // Use this for initialization
    void Start () {
        curEst = Estados.Patrolling;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (curEst)
        {
            case Estados.Patrolling:
                Patrolling(); break;
            case Estados.Chase:
                Chase(); break;
            case Estados.Attacking:
                Attack(); break;
        }
	}

    void Patrolling()
    {
        agent.destination = wayPoints[wpIndex].position;
       // Debug.Log(Vector3.Distance(transform.position, wayPoints[wpIndex].position));
        if (Vector3.Distance(transform.position, wayPoints[wpIndex].position)<0.5f)
        {
           
            if (wayPoints.Length == wpIndex+1)
                wpIndex = 0;
            else
                wpIndex++;
        }
    }
    
    void Chase()
    {
        if (Vector3.Distance(transform.position, objective.position) < distanceAttack)
            curEst = Estados.Attacking;
        else
            agent.SetDestination(objective.position);
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, objective.position) > distanceAttack)
            curEst = Estados.Chase;
        else
            StartCoroutine(waitAndAtack(coolDown));
    }
    bool aux = true;
    private IEnumerator waitAndAtack(float waitTime)
    {
        while (aux)
        {
            aux = false;
            yield return new WaitForSeconds(waitTime);
            playerhealth.doDamage(damage);
            aux = true;
        }
    }

    public void setObjective(Transform setObjective)
    {
       // Debug.Log("OBJJJJ");
        if(setObjective == null)
            curEst = Estados.Patrolling;
        else
        {
            objective = setObjective;
            curEst = Estados.Chase;
        }
    }
}
