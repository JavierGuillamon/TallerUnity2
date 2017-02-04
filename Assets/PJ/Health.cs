using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    [SerializeField]
    private float health;

    public Image fadeImg;
    public Text fadeText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void doDamage(float damage)
    {
        Debug.Log("Daño " + damage + " Vida: " + health);
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        while (fadeImg.color.a < 1)
        {
            Color auxI = fadeImg.color;
            Color auxT = fadeText.color;
            auxI.a = auxI.a + 0.01f;
            auxT.a = auxT.a + 0.01f;
            fadeImg.color = auxI;
            fadeText.color = auxT;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
