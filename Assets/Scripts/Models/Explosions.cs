using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour {

    public bool doesDamage;
    public bool isEnemy; //para saber si la ha provocado el enemigo o el player
    public float durationTime = 1;
    private CircleCollider2D circleCollider2D;
    
	void Start () {
        circleCollider2D = GetComponent<CircleCollider2D>();
        if (doesDamage == false)
        {
            Destroy(circleCollider2D);
        }
	}
	
	void Update () {
        durationTime -= Time.deltaTime;
        if (durationTime < 0) Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "ImanA" || collider.gameObject.tag == "ImanB")
        {
            ImanAController pc = collider.gameObject.GetComponent<ImanAController>();

        }
    }
}
