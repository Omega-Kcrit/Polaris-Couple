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
                Iman.GetComponent<Rigidbody2D>().transform.position = this.transform.position - /*new Vector3(0, 1.3f, 0);*/ new Vector3(0, Iman.GetComponent<SpriteRenderer>().bounds.extents.y , 0);
                    
                    //.sprite.bounds.extents.y,0);
                    //GetComponent<Sprite>().associatedAlphaSplitTexture.height/2,0);
                    /*new Vector3 (0,Iman.GetComponent<Rigidbody2D>().centerOfMass.y/2,0)*/
                    /*new Vector3(0,this.gameObject.GetComponent<CircleCollider2D>().radius,*//*0)*/ /*new Vector3(0, (Iman.GetComponent<CapsuleCollider2D>().size.y / 10), 0)*/;

                Iman.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                
                anclajeJoint.enabled = true;
                this.anclajeJoint.distance = startDistance;

                //anclajeSpringJoint.enabled = true;
            }
            else
            {
                print("outPnedul");
                Iman.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;            
                anclajeJoint.enabled = false;
                //anclajeSpringJoint.enabled = false;
                Iman = null;
                print("desinmatizado");
                imantizado = false;
            }
        }
      

    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        //if (coll.GetComponent<ImanAController>() != null)
        //{
        //    if (Input.GetKey("q") || InputManager.YButtonGetKey())
        //    {
        //        if (coll.GetComponent<ImanAController>().InControllA)
        //        {
        //            this.anclajeJoint.distance = startDistance;
        //        }
        //    }
        //}
        //else if (coll.GetComponent<ImanBController>() != null)
        //{
        //    if (Input.GetKey("q") || InputManager.YButtonGetKey())
        //    {
        //        if (coll.GetComponent<ImanBController>().InControllB)
        //        {
        //            this.anclajeJoint.distance = startDistance;
        //        }
        //    }
        //}
    }

    public void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.GetComponent<ImanAController>() != null)
        {
            if (Input.GetKey("q") || InputManager.YButtonGetKey())
            {
                if (coll.GetComponent<ImanAController>().InControllA)
                {
                    

                    //ballAControll.GetComponent<Rigidbody2D>().transform.position = this.transform.position;
                    Iman = coll.gameObject;

                    this.anclajeJoint.connectedBody = ballBControll.GetComponent<Rigidbody2D>();
                    this.anclajeJoint.connectedAnchor = new Vector2(0, ballBControll.GetComponent<SpriteRenderer>().bounds.extents.y);
                    this.anclajeJoint.distance = startDistance;

                    //this.anclajeSpringJoint.connectedBody = ballBControll.GetComponent<Rigidbody2D>();
                    //this.anclajeSpringJoint.distance = startDistance;
                    
                    ballBControll.GetComponent<ImanBController>().inPendulo = true;
                }
                if (!imantizado)
                {
                    print("Cambio hecho");
                    imantizado = true;
                    swapper.change();

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

                    //ballBControll.GetComponent<Rigidbody2D>().transform.position = this.transform.position;

                    Iman = coll.gameObject;

                    this.anclajeJoint.connectedBody = ballAControll.GetComponent<Rigidbody2D>();
                    this.anclajeJoint.connectedAnchor = new Vector2(0, ballAControll.GetComponent<SpriteRenderer>().bounds.extents.y);
                    this.anclajeJoint.distance = startDistance;

                    //this.anclajeSpringJoint.connectedBody = ballAControll.GetComponent<Rigidbody2D>();
                    //this.anclajeSpringJoint.distance = startDistance;
                    
                    ballAControll.GetComponent<ImanAController>().inPendulo = true;
                }
                if (!imantizado)
                {
                    print("Cambio hecho");
                    imantizado = true;
                    swapper.change();

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

    public void MovmentInJoint()
    {
        if (anclajeJoint != null && anclajeJoint.enabled)
        {
            if (Input.GetKey("w") || InputManager.MainVertical() > 0f)
            {
                if (anclajeJoint.distance <= minDist)
                {
                    anclajeJoint.distance = minDist;
                }
                else
                {
                    anclajeJoint.distance -= 0.2f;
                }                
            }
            if (Input.GetKey("s") || InputManager.MainVertical() < 0f)
            {
                if (anclajeJoint.distance >= maxDist)
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