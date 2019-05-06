using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atraccion : MonoBehaviour
{
    public GameObject Cop;
    public float step = 0.2f;
    public Rigidbody2D rb;
    public float distanciaA = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(this.GetComponent<Transform>().position, Cop.GetComponent<Transform>().position) >distanciaA)
        {
            float distance = Vector2.Distance(Cop.GetComponent<Transform>().position, transform.position);
            float speed = (distance - distanciaA) * 3;
            rb.AddForce((Cop.GetComponent<Transform>().position - transform.position).normalized * speed);
        }
        else if(Vector3.Distance(this.GetComponent<Transform>().position, Cop.GetComponent<Transform>().position) < distanciaA)
        {
            float distance = Vector2.Distance(Cop.GetComponent<Transform>().position, transform.position);
            float speed = -(distance - distanciaA) * 3;
            rb.AddForce((Cop.GetComponent<Transform>().position - transform.position).normalized * speed);

        }
        if (Vector3.Distance(this.GetComponent<Transform>().position, Cop.GetComponent<Transform>().position) > 10f)
        {
            float distance = Vector2.Distance(Cop.GetComponent<Transform>().position, transform.position);
            float speed = 10;
            rb.AddForce((Cop.GetComponent<Transform>().position - transform.position).normalized * speed);
        }
       
    }
    public void Iman()
    {
        float a = Vector3.Distance(this.GetComponent<Transform>().position, Cop.GetComponent<Transform>().position);
        Debug.Log(a);
        if (Vector3.Distance(this.GetComponent<Transform>().position, Cop.GetComponent<Transform>().position) > 3.5)
        {
            float distance = Vector2.Distance(Cop.GetComponent<Transform>().position, transform.position);
            float speed = 3.5f - distance;
            rb.AddForce((Cop.GetComponent<Transform>().position - transform.position).normalized * speed);
        }

    }
}
