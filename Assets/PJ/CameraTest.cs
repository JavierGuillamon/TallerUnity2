using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private PJMovementControll pjmc;
    [SerializeField]
    private List<Transform> targets;
    [SerializeField]
    private Transform selectedTarget;

    private Transform tr;

 //   [SerializeField]
  //  private float anglePerSecond;
    [SerializeField]
    private float reduceVelocityTime = .25f;

    //Materials for targets.
    public Material[] materials;

    public Transform target;
    public float distance = 6.0f;

    public float bufferup = 1.5f;
    public float bufferright = 0.75f;

    public float xSpeed = 120.0f;
    public float ySpeed = 100.0f;

    public float x_Angle_Izq = -40f;
    public float x_Angle_Der = 40f;

    public float y_Angle_Min = 0f;
    public float y_Angle_Max = 80f;

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 angles;
    // Use this for initialization
    void Start()
    {
        tr = transform;

        Cursor.lockState = CursorLockMode.Locked;
        angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
       // if (GetComponent<Rigidbody>())
            //GetComponent<Rigidbody>().freezeRotation = true;
    }
  
    void Update()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            distance -= .5f * Input.mouseScrollDelta.y;
            if (distance < 0)
            {
                distance = 0;
            }
           
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

           Vector3 axis= pjmc.axisSign;

            // x = ClampAngle(x, x_Angle_Izq, x_Angle_Der);
          
            angles = transform.eulerAngles;
            float angle = target.eulerAngles.y - angles.y;
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            angle = Mathf.Abs(angle);
            if((angle >0  && angle <45)||(angle >315 && angle<360 ))
             {
                 x += Input.GetAxis("Mouse X") * xSpeed * reduceVelocityTime * 0.02f;
             }
             else
             {
                 x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
             }
             
           // x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y = ClampAngle(y, y_Angle_Min, y_Angle_Max);

                //Quaternion rotation = Quaternion.Euler(y, x, 0);
                Vector3 position = rotation * new Vector3(bufferright, 0.0f, -distance) + target.position + new Vector3(0.0f, bufferup, 0.0f);

            //transform.rotation = Quaternion.AngleAxis(anglePerSecond* Time.deltaTime, Vector3.forward);
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

}