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
    private Transform followXForm;
    [SerializeField]
    private Vector3 offset = new Vector3(0f,1.5f,0f);

    private Vector3 lookDir;
    private Vector3 targetPosition;

    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;

	// Use this for initialization
	void Start () {
        if(followXForm == null)
            followXForm = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate () {
        Vector3 characterOffset = followXForm.position + offset;
        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        Debug.DrawRay(this.transform.position, lookDir, Color.green);

        targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceForward;
        smoothPosition(this.transform.position, targetPosition);

      /*  //cogemos la posicion del personaje con follow.position, le sumamos el modificador de altura para subir o bajar el angulo de la camara y le restamos la distancia que queremos que se aleje del personaje
        targetPosition = followXForm.position + followXForm.up * distanceUp - followXForm.forward * distanceForward;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(followXForm);*/
    }

    private void smoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

}
