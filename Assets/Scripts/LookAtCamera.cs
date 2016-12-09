using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public Camera camera;
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(camera.transform.position, -Vector3.up);
	}
}
