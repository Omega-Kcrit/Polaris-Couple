using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobilePlatformScript : MonoBehaviour {

   
    private bool playerOnPlatform = false;
    //private Rigidbody2D rb2d;

    public float verticalForce = 7;
    public Transform topeVertical;
    public float speed = 0.5f;

    private float initialY;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
        initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        if (playerOnPlatform && transform.position.y < topeVertical.position.y)
        {
            transform.position += new Vector3(0, verticalForce * Time.fixedDeltaTime * speed);
        }
        else if(transform.position.y > initialY && !playerOnPlatform)
        {
            transform.position -= new Vector3(0, verticalForce * Time.fixedDeltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB") playerOnPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ImanA" && collision.gameObject.tag == "ImanB") playerOnPlatform = false;
    }
}
