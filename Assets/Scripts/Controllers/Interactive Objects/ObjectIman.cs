using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIman : MonoBehaviour
{

    public float speed= 2;
    public float direction = 1;
    public bool dedicated = false, target = false;
    public GameObject imanTarget;
    public bool focusToIman;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerStay2D(Collider2D coll)
    {
        GameObject col = coll.gameObject;
        Debug.Log("Detectado");

        if (focusToIman)
        {
            if (coll.gameObject == imanTarget.gameObject)
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * direction);
            }  
        }

        //if (col.GetComponent<ImanAController>() != null)
        //{
        //    if (dedicated)
        //    {
        //        if (target)
        //        {
        //            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        //            rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * direction);
        //        }
        //        else
        //        {
        //            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        //            rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * -direction);
        //        }
        //    }
        //    else
        //    {
        //        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        //        rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * direction);
        //    }
                

        //}

        //if (col.GetComponent<ImanBController>() != null)
        //{
        //    if (dedicated)
        //    {
        //        if (!target)
        //        {
        //            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        //            rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * direction);
        //        }
        //        else
        //        {
        //            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        //            rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * -direction);
        //        }
        //    }
        //    else
        //    {
        //        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        //        rb.AddForce((col.GetComponent<Transform>().position - transform.position).normalized * speed * direction);
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        
            Debug.Log("exit");
        
    }

}
