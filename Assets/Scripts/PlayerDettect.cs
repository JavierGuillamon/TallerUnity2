using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDettect : MonoBehaviour {
    public EnemyAI ai;
	
    void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
            ai.setEstAttck(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            ai.setEstAttck(false);
    }
}
