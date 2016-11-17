using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PJMovementControll : MonoBehaviour {

    private Animator anim;
    public float corruptionLvl;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        Atack();
        Roll();
    }
    
    //Metodo que actualiza los valores de movimiento
    void Move()
    {
        //Valor de movimiento adelante y atras
        anim.SetFloat("Forward", Input.GetAxis("Vertical"));
        //Valor de giro izquieda y derecha
        anim.SetFloat("Turn", Input.GetAxis("Horizontal"));
        
        //añadir movimiento con camara fija (strafe)


    }

    void Atack()
    {
        anim.SetBool("Atack", Input.GetMouseButtonDown(0));
        //Aplicar el nivel de corrupcion o lo que sea para decidir la animacion del ataque que se va a realizar
        anim.SetFloat("CorruptionLvl", corruptionLvl);
    }

    void Roll()
    {
        if(Input.GetAxis("Vertical")>.1)
            anim.SetBool("Roll", Input.GetKey(KeyCode.LeftShift));
    }

}
