using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anclajes : MonoBehaviour
{

    public GameObject Iman;
    public PlayerSwapper swapper;
    // Start is called before the first frame update
    void Start()
    {
        swapper = GameObject.Find("Main Camera").GetComponent<PlayerSwapper>();
    }

    // U    pdate is called once per frame
    void Update()
    {
        if (Iman != null)
        {
            if (Input.GetKey("q"))
            {
                Iman.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                Iman.GetComponent<DistanceJoint2D>().enabled = true;
            }
            else
            {
                Iman.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Iman.GetComponent<DistanceJoint2D>().enabled = false;
                Iman = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        GameObject col = coll.gameObject;

        if (col.GetComponent<ImanAController>() != null)
        {
            if (Input.GetKey("q"))
            {
                Iman = col;
                if (col.GetComponent<ImanAController>().InControllA)
                {
                    
                    swapper.change();
                }
            }
        }
        else if (col.GetComponent<ImanBController>() != null)
        {
            if (Input.GetKey("q"))
            {
                if (col.GetComponent<ImanBController>().InControllB)
                {
                    Iman = col;
                    if (col.GetComponent<ImanBController>().InControllB)
                    {
                        swapper.change();
                    }
                }

            }
        }


    }
}
