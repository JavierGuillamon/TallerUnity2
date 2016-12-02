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
    //For unlock
    private float LastTap = 0;
    [SerializeField]
    private float TapTime;
    private int numberEnemys = 0;
    private int enemySelect = -1;

    bool locked = false;

    private Transform tr;//transform del propio personaje

    //bool locked = false;

    //lista de materiales de prueba para la seleccion de target
    public Material[] materials;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        targets = new List<Transform>();
        tr = transform;
        findEnemies();

    }
	
	// Update is called once per frame
	void Update () {
        Target();
    }
    private void Target()
    {

        if (locked) TargetEnemy(enemySelect);
       
        if (Input.GetKeyDown(KeyCode.Tab))
        {
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
                pjMovCont.canTurn = false;
                locked = true;
                if (locked)
                {
                    enemySelect++;
                    if (enemySelect >= numberEnemys)
                        enemySelect = 0;
                    TargetEnemy(enemySelect);
                }
                LastTap = Time.time;
            }

        }
        
    }
    void findEnemies()
    {
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

    private void TargetEnemy(int focus)
    {
        if(selectedTarget != null)
        {
           selectedTarget.GetComponentInParent <Renderer>().sharedMaterial = materials[0];
        }
       
        selectedTarget = targets[focus];
        selectedTarget.GetComponentInParent<Renderer>().sharedMaterial = materials[1];


        Vector3 lookAtPosition = selectedTarget.position;
        lookAtPosition.y = transform.position.y;

        //Quaternion rotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        //transform.rotation = Quaternion.Lerp(tr.rotation, rotation, Time.deltaTime * smooth);

        transform.LookAt(lookAtPosition);

    }
}
