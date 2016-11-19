using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PJMovementControll : MonoBehaviour {

    private Animator anim;
    private Transform trans;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Turn();
        Move();
        Atack();
        Roll();
    }
    
    void Turn()
    {
        anim.SetFloat("Turn", Input.GetAxis("Horizontal"));
    }

    //Metodo que actualiza los valores de movimiento
    void Move()
    {
        //Valor de movimiento adelante y atras
        anim.SetFloat("MoveX", Input.GetAxis("MoveX"));
        anim.SetFloat("MoveZ", Input.GetAxis("MoveZ"));
    }

    void Atack()
    {
        anim.SetBool("Atack", Input.GetMouseButton(0));
      
        //Aplicar el nivel de corrupcion o lo que sea para decidir la animacion del ataque que se va a realizar
        //anim.SetFloat("CorruptionLvl", corruptionLvl);
    }

    void Roll()
    {
        if(Input.GetAxis("MoveZ")>.1)
            anim.SetBool("Roll", Input.GetKey(KeyCode.LeftShift));
    }

    
   
}
