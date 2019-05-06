using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public GameObject ballAControll;
    public GameObject ballBControll;

    public bool inPendulu = false;
    public Collider2D colliderA;

    public bool bottomPressUp;

    public GameObject[] ballArray;
    private int ballIndex = 0;

    public PlayerSwapper swapper;
 
    public DistanceJoint2D anclajeJoint;
    public SpringJoint2D anclajeSpringJoint;

    private bool imantizado = false;
    private GameObject Iman;

    public float startDistance;
    public float minDist;
    public float maxDist;

    public bool addImpulse;
    public float swingImpulse = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        ballIndex = 0;

        anclajeJoint = this.GetComponent<DistanceJoint2D>();
        anclajeJoint.enabled = false;

        //anclajeSpringJoint = this.GetComponent<SpringJoint2D>();
        //anclajeSpringJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovmentInJoint();

        if (Iman != null)
        {
            if (Input.GetKey("q") || InputManager.YButtonGetKey())
            {
                Iman.GetComponent<Rigidbody2D>().transform.position = this.transform.position;

                Iman.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                anclajeJoint.enabled = true;

                //anclajeSpringJoint.enabled = true;

            }
            else
            {
                print("outPnedul");
                Iman.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                anclajeJoint.enabled = false;
                //anclajeSpringJoint.enabled = false;

                Iman = null;

            }
        }

    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<ImanAController>() != null)
        {
            if (Input.GetKey("q") || InputManager.YButtonGetKey())
            {
                if (coll.GetComponent<ImanAController>().InControllA)
                {
                    this.anclajeJoint.distance = startDistance;
                }

            }
        }
        else if (coll.GetComponent<ImanBController>() != null)
        {
            if (Input.GetKey("q") || InputManager.YButtonGetKey())
            {
                if (coll.GetComponent<ImanBController>().InControllB)
                {
                    this.anclajeJoint.distance = startDistance;
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.GetComponent<ImanAController>() != null)
        {
            if (Input.GetKey("q") || InputManager.YButtonGetKey())
            {
                if (coll.GetComponent<ImanAController>().InControllA)
                {
                    Debug.Log("Cambio hecho por pendulo 1");
                    if (!imantizado)
                    {
                        swapper.change();
                        imantizado = true;

                    }

                    //ballAControll.GetComponent<Rigidbody2D>().transform.position = this.transform.position;
                    Iman = coll.gameObject;

                    this.anclajeJoint.connectedBody = ballBControll.GetComponent<Rigidbody2D>();
                    this.anclajeJoint.distance = startDistance;

                    //this.anclajeSpringJoint.connectedBody = ballBControll.GetComponent<Rigidbody2D>();
                    //this.anclajeSpringJoint.distance = startDistance;

                    swapper.change();
                    ballBControll.GetComponent<ImanBController>().inPendulo = true;
                }

            }
            else
            {
                if (addImpulse)
                {
                    print("penduloImpulseB");
                    ballBControll.GetComponent<ImanBController>().rb2D.AddForce(new Vector2(ballBControll.GetComponent<ImanBController>().transform.right.normalized.x * swingImpulse, ballBControll.GetComponent<ImanBController>().transform.up.normalized.y * swingImpulse), ForceMode2D.Impulse);
                    //ballBControll.GetComponent<ImanBController>().rb2D.AddForce(new Vector2(ballBControll.GetComponent<ImanBController>().rb2D.velocity.normalized.x * 0.5f, ballBControll.GetComponent<ImanBController>().rb2D.velocity.normalized.y *0.5f), ForceMode2D.Impulse);
                    //ballBControll.GetComponent<ImanBController>().rb2D.AddForce(new Vector2(0.5f,0.5f), ForceMode2D.Impulse);
                }
                ballBControll.GetComponent<ImanBController>().inPendulo = false;

            }


        }
        else if (coll.GetComponent<ImanBController>() != null)
        {
            if (Input.GetKey("q") || InputManager.YButtonGetKey())
            {
                if (coll.GetComponent<ImanBController>().InControllB)
                {
                    Debug.Log("Cambio hecho por pendulo 2");
                    if (!imantizado)
                    {
                        swapper.change();
                        imantizado = true;

                    }

                    //ballBControll.GetComponent<Rigidbody2D>().transform.position = this.transform.position;

                    Iman = coll.gameObject;

                    this.anclajeJoint.connectedBody = ballAControll.GetComponent<Rigidbody2D>();
                    this.anclajeJoint.distance = startDistance;

                    //this.anclajeSpringJoint.connectedBody = ballAControll.GetComponent<Rigidbody2D>();
                    //this.anclajeSpringJoint.distance = startDistance;

                    swapper.change();
                    ballAControll.GetComponent<ImanAController>().inPendulo = true;
                }
            }
            else
            {
                
                if (addImpulse)
                {
                    print("penduloImpulseA");
                    ballAControll.GetComponent<ImanAController>().rb2D.AddForce(new Vector2(ballAControll.GetComponent<ImanAController>().transform.right.normalized.x * swingImpulse, ballAControll.GetComponent<ImanAController>().transform.up.normalized.y * swingImpulse), ForceMode2D.Impulse);
                    //ballAControll.GetComponent<ImanAController>().rb2D.AddForce(new Vector2(ballAControll.GetComponent<ImanAController>().rb2D.velocity.normalized.x * 0.5f, ballAControll.GetComponent<ImanAController>().rb2D.velocity.normalized.y * 0.5f), ForceMode2D.Impulse);
                    //ballAControll.GetComponent<ImanAController>().rb2D.AddForce(new Vector2 (0.5f,0.5f), ForceMode2D.Impulse);
                }
                ballAControll.GetComponent<ImanAController>().inPendulo = false;

            }

        }
    }
    public void OnTriggerExit(Collider other)
    {
        imantizado = false;
    }

    public void MovmentInJoint()
    {
        if (anclajeJoint != null && anclajeJoint.enabled)
        {
            if (Input.GetKey("w") || InputManager.MainVertical() > 0f)
            {
                if (anclajeJoint.distance < minDist)
                {
                    anclajeJoint.distance = minDist;
                }
                else
                {
                    anclajeJoint.distance -= 0.2f;
                }                
            }
            if (Input.GetKey("s")|| InputManager.MainVertical() < 0f)
            {
                if (anclajeJoint.distance > maxDist)
                {
                    anclajeJoint.distance = maxDist;
                }
                else
                {
                    anclajeJoint.distance += 0.2f;
                }
            }
        }

        //if (anclajeSpringJoint != null && anclajeSpringJoint.enabled)
        //{
        //    if (Input.GetKey("w") || InputManager.MainVertical() > 0f)
        //    {
        //        if (anclajeSpringJoint.distance<minDist)
        //        {
        //            anclajeSpringJoint.distance = minDist;
        //        }
        //        else
        //        {
        //            anclajeSpringJoint.distance -= 0.2f;
        //        }
        //    }

        //    if (Input.GetKey("s") || InputManager.MainVertical() < 0f)
        //    {
        //        if (anclajeSpringJoint.distance > maxDist)
        //        {
        //            anclajeSpringJoint.distance = maxDist;
        //        }
        //        else
        //        {
        //            anclajeSpringJoint.distance += 0.2f;
        //        }
        //    }


            
        //}
    }
}    