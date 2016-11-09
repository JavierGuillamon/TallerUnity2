using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PJMovementControll : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }
    
    //Metodo que actualiza los valores de movimiento
    void Move()
    {
        //Valor de movimiento adelante y atras
        anim.SetFloat("Forward", Input.GetAxis("Vertical"));
        //Valor de giro izquieda y derecha
        anim.SetFloat("Turn", Input.GetAxis("Horizontal"));
        
    }
}
