using UnityEngine;
using System.Collections;

public class Attack_Script : MonoBehaviour {
    public PJMovementControll control;
    public BoxCollider coll;
    public DetectEnemy detect;
    public enum DamageType { BASIC, HARD}

    [System.Serializable]
    public struct Player_Attack
    {
        public Vector3 offset;
        public Vector3 size;
        public int damage;
        public DamageType damageType;
    }

    public  Player_Attack[] attacks;

    public void Attack(int attack)
    {

        coll.enabled = !coll.enabled;
        control.setAttacking();
        detect.setDamage(attacks[attack].damage);
        //Debug.Log("atacando: " + control.getAttacking()+" doing: "+attacks[attack].damage);
    }

}
