using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Traget : MonoBehaviour {
    [SerializeField]
    private List<Transform> targets;
    [SerializeField]
    private Transform selectedTarget;

    private Transform tr;//transform del propio personaje

    //lista de materiales de prueba para la seleccion de target
    public Material[] materials;

    // Use this for initialization
    void Start () {
        targets = new List<Transform>();
        tr = transform;
        findEnemies();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TargetEnemy();
        }
    }

    void findEnemies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject enemy in go)
        {
            targets.Add(enemy.transform);
        }
    }
    private void SortTargets()
    {
        targets.Sort(delegate (Transform t1, Transform t2)
        {
            return (Vector3.Distance(t1.position, tr.position).CompareTo(Vector3.Distance(t2.position, tr.position)));
        });
    }

    private void TargetEnemy()
    {
        if(selectedTarget != null)
        {
           selectedTarget.GetComponentInParent <Renderer>().sharedMaterial = materials[0];
        }
        SortTargets();
        selectedTarget = targets[0];
        selectedTarget.GetComponentInParent<Renderer>().sharedMaterial = materials[1];
    }
}
