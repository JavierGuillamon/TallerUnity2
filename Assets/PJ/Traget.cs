using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Traget : MonoBehaviour {
    [SerializeField]
    private List<Transform> targets;
    [SerializeField]
    private Transform selectedTarget;
    [SerializeField]
    private float smooth;
    private Animator anim;
    [SerializeField]
    PJMovementControll pjMovCont;
    [SerializeField]
    private float speedRotation;
    //For unlock
    private float LastTap = 0;
    [SerializeField]
    private float TapTime;
    private int numberEnemys = 0;
    private int enemySelect = -1;

    bool locked = false;

    private Transform tr;//transform del propio personaje

    //lista de materiales de prueba para la seleccion de target
    [SerializeField]
    private Material[] materials;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        tr = transform;
        findEnemies();
    }
	
	// Update is called once per frame
	void Update () {
        Target();
    }

    private void Target()
    {
        
        if (locked) TargetEnemy(enemySelect,0);
       
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            findEnemies();
            SortTargets();
            if (Time.time - LastTap < TapTime)
            {
                anim.SetBool("Target", false);
                pjMovCont.canTurn = true;
                locked = false;
                selectedTarget.GetComponentInParent<Renderer>().sharedMaterial = materials[0];
                enemySelect = 0;
            }
            else {
                anim.SetBool("Target", true);
                anim.SetFloat("Turn", 0);
                pjMovCont.canTurn = false;
                locked = true;
                if (locked)
                {
                    enemySelect++;
                    if (enemySelect >= numberEnemys)
                        enemySelect = 0;
                    TargetEnemy(enemySelect,1);
                }
                LastTap = Time.time;
            }
        }       
    }
    public void stopTarget()
    {
        anim.SetBool("Target", false);
        pjMovCont.canTurn = true;
        locked = false;
        enemySelect = 0;
    }
    public void findEnemies()
    {
        targets = new List<Transform>();
        numberEnemys = 0;
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject enemy in go)
        {
            targets.Add(enemy.transform);
            numberEnemys++; 
        }
    }
    private void SortTargets()
    {
        targets.Sort(delegate (Transform t1, Transform t2)
        {
            return (Vector3.Distance(t1.position, tr.position).CompareTo(Vector3.Distance(t2.position, tr.position)));
        });
    }

    private void TargetEnemy(int focus, int tap)
    {
        if(selectedTarget != null)
        {
           selectedTarget.GetComponentInParent <Renderer>().sharedMaterial = materials[0];
        }
       
        selectedTarget = targets[focus];
        selectedTarget.GetComponentInParent<Renderer>().sharedMaterial = materials[1];


        Vector3 lookAtPosition = selectedTarget.position;
        lookAtPosition.y = transform.position.y;

        if (tap == 0)
        {
            transform.LookAt(lookAtPosition);
        }
        else
        {
            float step = speedRotation * Time.deltaTime;
            Quaternion rotation = Quaternion.LookRotation(lookAtPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
            Debug.Log("···");
        }
    }
    
}
