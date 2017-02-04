using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDettect : MonoBehaviour {
    [SerializeField]
    private EnemyAI ai;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ai.setObjective(other.gameObject.transform);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ai.setObjective(null);
        }
    }
}
