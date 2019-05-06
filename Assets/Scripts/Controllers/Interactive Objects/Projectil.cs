using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    public bool target = false;
    public float speed = 1f;
    public float speedA = 0.5f;
    public float direction ;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.AddForce((transform.position).normalized * speed*direction);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        
        GameObject col = coll.gameObject;
        if(col.GetComponent<ImanAController>() != null|| col.GetComponent<ImanBController>() != null)
        {
            ImanAController imanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
            ImanBController imanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
            CheckPointMaster cpm = FindObjectOfType(typeof(CheckPointMaster)) as CheckPointMaster;
            imanA.transform.position = cpm.lastCheckPoint + new Vector2(2f, 0);
            imanB.transform.position = cpm.lastCheckPoint - new Vector2(2f, 0);
        }
        Destroy(gameObject);

    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        GameObject col = coll.gameObject;

        if (col.GetComponent<ImanAController>() != null)
        {
            if (direction < 0 && col.transform.position.x<transform.position.x)//Si esta en direccion la direccion de disparo y no lo ha adelantado
            {
                if (target)//Si Target es true va a por el Iman A si no va a por este se distanciara
                {
                    rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speedA * direction);
                }else
                {
                    rb.AddForce(-(col.GetComponent<Transform>().position - transform.position).normalized * speedA*2 * direction);
                }
                
            }
            if (direction > 0 && col.transform.position.x > transform.position.x)//Si esta en direccion la direccion de disparo y no lo ha adelantado
            {
                if (target)//Si Target es true va a por el Iman A si no va a por este se distanciara
                {
                    rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speedA * direction);
                }
                else
                {
                    rb.AddForce(-(col.GetComponent<Transform>().position - transform.position).normalized * speedA * 2 * direction);
                }

            }

        }
        if (col.GetComponent<ImanBController>() != null)
        {
            if (direction < 0 && col.transform.position.x < transform.position.x)//Si esta en direccion la direccion de disparo y no lo ha adelantado
            {
                if (target)//Si Target es true va a por el Iman B si no va a por este se distanciara
                {
                    rb.AddForce(-(col.GetComponent<Transform>().position - transform.position).normalized * speedA * direction);
                }
                else
                {
                    rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speedA * 2 * direction);
                }
            }
            if (direction > 0 && col.transform.position.x > transform.position.x)//Si esta en direccion la direccion de disparo y no lo ha adelantado
            {
                if (target)//Si Target es true va a por el Iman B si no va a por este se distanciara
                {
                    rb.AddForce(-(col.GetComponent<Transform>().position - transform.position).normalized * speedA * direction);
                }
                else
                {
                    rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speedA * 2 * direction);
                }
            }
        }
    }
}
