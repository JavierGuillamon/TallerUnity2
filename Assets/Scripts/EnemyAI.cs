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
    public Transform Player;
    public Transform[] wayPoints;
    NavMeshAgent agent;
    public float distanceAttack;
    private Estados curEst;
    private int wpIndex=0;
    private Transform curWP;

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
        Debug.Log(Vector3.Distance(transform.position, wayPoints[wpIndex].position));
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
        if (Vector3.Distance(transform.position, Player.position) < distanceAttack)
            curEst = Estados.Attacking;
        else
            agent.SetDestination(Player.position);
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, Player.position) > distanceAttack)
            curEst = Estados.Chase;
        else
            Debug.Log("PEW PEW AAAA OUUUCH");
    }

    public void setEstAttck(bool est)
    {
        if (est)
        {
            curEst = Estados.Chase;
        }
        else
        {
            curEst = Estados.Patrolling;
        }
    }
}
