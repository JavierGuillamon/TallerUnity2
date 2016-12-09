using UnityEngine;
using System.Collections;

public class DetectEnemy : MonoBehaviour {

    private int damage;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Damage(damage);
        }
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }
}
