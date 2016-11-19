using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
    [SerializeField]
    private float distanceForward;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform follow;
    private Vector3 targetPosition;


	// Use this for initialization
	void Start () {
           follow = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceForward;
        Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
        Debug.DrawRay(follow.position, follow.forward * distanceForward, Color.blue);
        Debug.DrawRay(follow.position, targetPosition, Color.magenta);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(follow);
    }

    void LateUpdate()
    {
       
    }
}
