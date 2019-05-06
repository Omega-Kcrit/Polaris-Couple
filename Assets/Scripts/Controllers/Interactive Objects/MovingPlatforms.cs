using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    float t; //valor arbitrario que nos servirá para que se mueva la plataforma a través del tiempo
    public bool isVertical;
    [HideInInspector]public bool keepMoving = true;

    public Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;                                                                                                       //Los valores hardcodeados definen la longitud del desplazamiento de la plataforma
                                                                                                                                        //Está la posibilidad de hacer una plataforma que se mueva hacia arriba o abajo hasta llegar a X punto. Sería solo modificar este código. Una vez esté el player se puede testear.
        if (keepMoving)
        {
            if (isVertical)
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y + Time.fixedDeltaTime);
                rb2d.AddForce(new Vector2(0, 10000f * Time.fixedDeltaTime), ForceMode2D.Force);

            }
            else
            {
                transform.position = new Vector2(transform.position.x + (Mathf.Sin(t * 1.5f) / 0.2f) * Time.deltaTime, transform.position.y);
            }
        }
    }                                                                                                                                   

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            collision.gameObject.transform.SetParent(transform); //para que el jugador al estar encima de la plataforma se mueva con ella
        }
        if (collision.gameObject.tag == "MovingPlatformStopper")
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
