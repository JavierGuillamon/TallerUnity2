using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private Traget target;

    private float health;
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private Image healthBar;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Damage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        Debug.Log("Health: " + health);
        if(health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("AAA");
            target.stopTarget();
        }
    }
}
