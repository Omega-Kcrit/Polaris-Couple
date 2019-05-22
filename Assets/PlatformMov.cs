using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMov : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    private bool playerOnPlatform = false;

    private Vector3 startingPos;

    [SerializeField]
    public bool platfInMov;
    public float speed = 1;
    [SerializeField]
    //private Transform childTransform;
    public Transform transformB;

    //Characters
    public GameObject imanA;
    public GameObject imanB;

    private bool imanAInPltf;
    private bool imanBInPltf;

    public bool platfForPlayers;

    private float cd = 20f;
    private bool setCd = false;

    private bool bossActivated = false;

    void Start()
    {
        //startingPos = this.transform.position;
        posA = transform.position;
        posB = transformB.position;

        nextPos = posB;

        imanA = GameObject.FindGameObjectWithTag("ImanA");
        imanB = GameObject.FindGameObjectWithTag("ImanB");
    }

    // Update is called once per frame
    void Update()
    {
        //if (!platfInMov)
        //{

        if (platfForPlayers)
        {
            if (imanAInPltf || imanBInPltf)
            {
                Move(true);
            }
            if (!imanAInPltf && !imanBInPltf)
            {
                Move(false);
            }
        }
        else if (bossActivated)
        {
            Move(true);
        }
        //Para que la plataforma vaya al respawn al morir el personaje
        //if (imanA.GetComponent<ImanAController>().imDeath || imanB.GetComponent<ImanBController>().imDeath)
        //{
        //    ToRespawn();
        //}
            //if (playerOnPlatform) Move();
        //}
        //else
        //{
        //    Move();
        //}
        //if (setCd) cd -= Time.deltaTime;
    }

    private void Move(bool inPlatf)
    {

        if (platfForPlayers)
        {
            if (inPlatf)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, posB, speed * Time.deltaTime);                                                                
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, posA, speed * Time.deltaTime);
            }
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextPos, speed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, nextPos) <= 0.1)
            {
                ChangeDestination();
            }
        }



        //this.transform.position = Vector3.MoveTowards(this.transform.position, nextPos, speed * Time.deltaTime);
        //if (Vector3.Distance (this.transform.position, nextPos)<=0.1)
        //{
        //    ChangeDestination();
        //}
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ImanA")
        {
            imanAInPltf = true;
        }
        if (collision.gameObject.tag == "ImanB")
        {
            imanBInPltf = true;
        }        


        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            //playerOnPlatform = true;
            //cd = 2f;
            //setCd = false;
            collision.gameObject.transform.SetParent(transform); //para que el jugador al estar encima de la plataforma se mueva con ella
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ImanA" )
        {
            imanAInPltf = false;
        }
        if (collision.gameObject.tag == "ImanB" )
        {
            imanBInPltf = false;
        }

        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            //playerOnPlatform = false;
            collision.gameObject.transform.SetParent(null);
            //setCd = true;
            //if ((cd >= 0 && (collision.gameObject.tag == "ImanA" && collision.gameObject.tag == "ImanB")) || this.gameObject.tag == "PlatfMov" || this.gameObject.tag == "VerticalMovingPlatform")
            //{
            //    this.transform.position = startingPos;
            //    cd = 20f;
            //}
        }
    }
    private void ToRespawn()
    {
        this.transform.position = posA;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB") bossActivated = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            bossActivated = false;
            this.transform.position = new Vector3(136.78f, -45.25f, 0);
        }
    }
}
