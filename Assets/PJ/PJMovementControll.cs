using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PJMovementControll : MonoBehaviour {
    [SerializeField]
    private CameraControll cameraCntrl;
    [SerializeField]
    private float directionSpeed=3.0f;
    [SerializeField]
    private float directionDampTime = 1.5f;
    [SerializeField]
    private float rotationDegreePerSecond = 120f;

    private Animator anim;
    private Transform trans;
    public bool canTurn=true;


    private float speed = 0.0f;
    private float direction = 0f;
    private float horizontal;
    private float vertical;
    private AnimatorStateInfo stateInfo;

    private int m_locomotionId = 0;

    //punto incial de camara, vector rojo
    public Vector3 axisSign;

    private bool attacking = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        m_locomotionId = Animator.StringToHash("Base Layer.Idle-Walk-Strafe");
	}
	
	// Update is called once per frame
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        StickToWorldSpace(this.transform, cameraCntrl.transform, ref direction, ref speed);
        Turn();
         Move();
         Attack();
         Roll();
/*
        if (canTurn && horizontal!=0)      
            anim.SetFloat("Turn", direction, directionDampTime, Time.deltaTime);
        else if(horizontal != 0)
            anim.SetFloat("MoveX", direction, directionDampTime, Time.deltaTime);

        anim.SetFloat("MoveZ", vertical);

        Debug.Log("Direccion: " + direction + " Speed: " + speed);*/
        //anim.SetFloat("MoveX",speed);

        //anim.SetFloat("MoveZ", direction, directionDampTime,Time.deltaTime);
    }
    
    void FixedUpdate()
    {
        if(IsInLocomotion()&& ((direction>=0 && horizontal>=0)|| (direction <0 && horizontal < 0)))
        {
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            this.transform.rotation = (this.transform.rotation * deltaRotation);
        }
    }

   void Turn()
    {
       if (canTurn)
        {
            anim.SetFloat("Turn", horizontal);
        }
    }

    //Metodo que actualiza los valores de movimiento
    void Move()
    {
        //Valor de movimiento adelante y atras
        if(!canTurn)
            anim.SetFloat("MoveX", horizontal);
        anim.SetFloat("MoveZ", vertical);
    }

    void Attack()
    {
       // if (!attacking)
       // {
            anim.SetBool("Atack", Input.GetMouseButton(0));
        //}
    }

    void Roll()
    {
        if(vertical>.1)
            anim.SetBool("Roll", Input.GetKey(KeyCode.LeftShift));
    }
    
   public void StickToWorldSpace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        speedOut = stickDirection.sqrMagnitude;

        //rotacion de la camara
        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

        //convertir input a worldspace
        Vector3 moveDirection = referentialShift * stickDirection;
        axisSign = Vector3.Cross(moveDirection, rootDirection);

        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), axisSign, Color.red);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
       // Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
       // angleRootToMove /= 180f;
        // directionOut = (angleRootToMove * directionSpeed);
       // directionOut = angleRootToMove;
        //Debug.Log("DIR OUT: " + directionOut);

        if(angleRootToMove>0 &&angleRootToMove <= 180)
        {
            directionOut = 1;
        }
        else
        {
            directionOut = -1;
        }
    }

    public bool IsInLocomotion()
    {
        return stateInfo.fullPathHash == m_locomotionId;
    }

    public void setAttacking()
    {        
        attacking = !attacking;
    }
    public bool getAttacking()
    {
        return attacking;
    }
}
