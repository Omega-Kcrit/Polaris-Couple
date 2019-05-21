using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImanBController : MonoBehaviour
{
    public PlayerModel playerModel;
    [HideInInspector] public Rigidbody2D rb2D;
    [HideInInspector] public SpriteRenderer spr;

    //Colliders de Repulsion
    /*[HideInInspector]*/
    public CircleCollider2D colForce2d;

    //GROUND POINT
    public Transform groundPoint;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;

    //IMAN B VALORES
    public bool InControllB = false;
    public ImanBState currentState;
    public bool CollidersEnable = true;

    //IMAN A VALORES
    public Transform imanATransform;
    public Rigidbody2D imanArb2d;
    public GameObject imanAObject;
    public Vector3 distanceImanA;

    //Repulsion y Atraccion Valores
    public bool inRepulsion = false;
    public float repulsionForce = 1f;
    public float velocityInRepulsion = 1f;
    public float repulsionForceStart = 1f;
    public float repulsionForceInCharm = 300f;
    public float maxVelocityInRepulsion = 1f;
    public float maxVelocityInImpulse = 10f;
    public bool switchMaxRepulsion = false;
    public bool inNormalRepulsion = false;
    public float maxNoramlVelocityInRepulsion = 1f;
    public float normalRepulsionForce = 1f;
    public float normalForceGlboal = 1f;

    //Charm Valores   
    public float speedMaxInCharm = 1;
    public float forceCharm = 20f;
    private bool charmActvie;

    //Fede: Cooldown para la habilidad de atraer
    public bool coolingdownSkill1 = false;
    public float cooldownCounterSkill1 = 3f;
    public float cooldownCounterStartSkill1 = 3f;


    //Pendulo 
    public bool inPendulo = false;
    public float massInPendulo = 1;
    public float speedMaxPendulo;


    //Animaciones
    public Animator animator;
    public GameObject Prefab_Bolagrande;
    public Vector3 PosPropulsio;
    public float time;
    public bool OnAir = false;

    //Luz
    public GameObject Luz;

    //Platf Mov
    private Transform transformPlayer;

    //Vida
    public bool imDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
        Invoke("SiguienteBola", time);

        playerModel = Instantiate(playerModel);

        if (InControllB)
        {
            ChangeState(new IBSGrounded(this));
            velocityInRepulsion = maxVelocityInImpulse;
        }
        else
        {
            ChangeState(new IBSNeutro(this));
        }

    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);
        distanceImanA = this.transform.position - imanATransform.position;
        animator.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));
        animator.SetFloat("SpeedUp",rb2D.velocity.y);
        animator.SetBool("OnAir", this.OnAir);
        CollisionEnable(CollidersEnable);

        Luz.gameObject.SetActive(InControllB);



    }
    public void SiguienteBola()
    {
        Instantiate(Prefab_Bolagrande, transform.position + PosPropulsio, Quaternion.identity);
        Invoke("SiguienteBola", time);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);

        if (InControllB)
        {
            if (!coolingdownSkill1)
            {
                Charm();
                cooldownCounterSkill1 = cooldownCounterStartSkill1;
            }
            else
            {
                cooldownCounterSkill1 -= Time.deltaTime;
                if (cooldownCounterSkill1 <= 0)
                {
                    coolingdownSkill1 = false;
                }
            }
        }

        if (!InControllB)
        {
            ChangeState(new IBSNeutro(this));
        }
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public void ChangeState(ImanBState ibs)
    {
        currentState = ibs;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanARepulsionArea")
        {
            inRepulsion = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanARepulsionArea")
        {
            //if (collision.gameObject.GetComponentInParent<Transform>().transform.position.y + (collision.gameObject.GetComponentInParent<CapsuleCollider2D>().size.y / 2) < this.transform.position.y - (this.GetComponent<CapsuleCollider2D>().size.y / 2))



            if (charmActvie)
            {
                print(this.currentState);
                this.rb2D.AddForce(distanceImanA.normalized * repulsionForce * normalForceGlboal, ForceMode2D.Force);
                inRepulsion = true;
                inNormalRepulsion = false;
            }
            else if (this.transform.position.y > collision.GetComponent<Transform>().position.y)
            {
                print(this.currentState);
                this.rb2D.AddForce(distanceImanA.normalized * repulsionForce * normalForceGlboal, ForceMode2D.Force);
                inRepulsion = true;
                inNormalRepulsion = false;
            }
            //Bueno
            //if ((this.transform.position.y > collision.GetComponent<Transform>().position.y) || (this.charmActvie))

            //{
            //    this.rb2D.AddForce(distanceImanA.normalized * repulsionForce, ForceMode2D.Force);
            //    inRepulsion = true;
            //}
            else
            {
                repulsionForce = normalRepulsionForce;
                this.rb2D.AddForce(distanceImanA.normalized * repulsionForce * normalForceGlboal, ForceMode2D.Force);
                inRepulsion = false;
                inNormalRepulsion = true;
            }

            //    this.rb2D.AddForce(distanceImanA.normalized * repulsionForce, ForceMode2D.Force);
            //inRepulsion = true;
        }
        if (collision.gameObject.tag == "PlatfMov")
        {
            if (this.transform.position.y > collision.gameObject.GetComponent<Transform>().position.y + (collision.gameObject.GetComponent<BoxCollider2D>().size.y / 2))
            {
                transformPlayer = collision.transform;
                this.transform.parent = transformPlayer.transform;
            }            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanARepulsionArea")
        {
            if (inNormalRepulsion)
            {
                velocityInRepulsion = maxNoramlVelocityInRepulsion;
                //repulsionForce = repulsionForceInCharm;
                float clampedSpeedX = Mathf.Clamp(this.rb2D.velocity.x, -velocityInRepulsion * normalForceGlboal, velocityInRepulsion * normalForceGlboal);
                float clampedSpeedY = Mathf.Clamp(this.rb2D.velocity.y, -velocityInRepulsion * normalForceGlboal, velocityInRepulsion * normalForceGlboal);
                this.rb2D.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
                inNormalRepulsion = false;
            }

            else if (switchMaxRepulsion)
            {
                velocityInRepulsion = maxVelocityInImpulse;
                repulsionForce = repulsionForceInCharm;
                float clampedSpeedX = Mathf.Clamp(this.rb2D.velocity.x, -velocityInRepulsion * normalForceGlboal, velocityInRepulsion * normalForceGlboal);
                float clampedSpeedY = Mathf.Clamp(this.rb2D.velocity.y, -velocityInRepulsion * normalForceGlboal, velocityInRepulsion * normalForceGlboal);
                this.rb2D.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
            }
            else        
            {
                velocityInRepulsion = maxVelocityInRepulsion;
                repulsionForce = repulsionForceStart;
                float clampedSpeedX = Mathf.Clamp(this.rb2D.velocity.x, -velocityInRepulsion * normalForceGlboal, velocityInRepulsion * normalForceGlboal);
                float clampedSpeedY = Mathf.Clamp(this.rb2D.velocity.y, -velocityInRepulsion * normalForceGlboal, velocityInRepulsion * normalForceGlboal);
                this.rb2D.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
            }
            
            inRepulsion = false;
            velocityInRepulsion = maxVelocityInRepulsion;
            repulsionForce = repulsionForceStart;
        }
        //if (collision.gameObject.tag == "ImpulseArea")
        //{
        //    skillEnable = false;
        //}
        if (collision.gameObject.tag == "PlatfMov")
        {
            this.transform.parent = null;
        }
    }
    

public void Charm()
{
    if (Input.GetKey("e") || InputManager.XButtonGetKey())
    {
            charmActvie = true;
        switchMaxRepulsion = true;
        if (!inRepulsion)
        {
            imanArb2d.gravityScale = 0;
            imanArb2d.AddForce(distanceImanA.normalized * forceCharm * normalForceGlboal, ForceMode2D.Force);

            float clampedSpeedX = Mathf.Clamp(imanArb2d.velocity.x, -speedMaxInCharm * normalForceGlboal, speedMaxInCharm * normalForceGlboal);
            float clampedSpeedY = Mathf.Clamp(imanArb2d.velocity.y, -speedMaxInCharm * normalForceGlboal, speedMaxInCharm * normalForceGlboal);
            imanArb2d.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
        }
        else
        {
            imanArb2d.gravityScale = 2;
            coolingdownSkill1 = true;
        }
    }
    else
    {
        imanArb2d.gravityScale = 2;
        velocityInRepulsion = maxVelocityInRepulsion;
        switchMaxRepulsion = false;
    }
    if (Input.GetKeyUp("e") || Input.GetButtonUp("X_Button"))
    {
        //coolingdownSkill1 = true;
        switchMaxRepulsion = false;
        imanArb2d.gravityScale = 2;
        charmActvie = false;
        }
    }


//public void Charm( )
//    {
//        if (/*habilidadAtraer > 0*/Input.GetKey("e"))
//        {
//            if (!inRepulsion)
//            {
//                imanArb2d.gravityScale = 0;
//                imanArb2d.AddForce(distanceImanA.normalized * charmSpeed * incrementSpeedCharm, ForceMode2D.Force);
//                if (incrementSpeedCharm < 10)
//                {
//                    incrementSpeedCharm = incrementSpeedCharm + incrementVelocityCharm;
//                }
//            }
//            else
//            {
//                incrementSpeedCharm = incrementVelocityCharm;
//                coolingdownSkill1 = true;
//                imanArb2d.gravityScale = 2;
//            }
//        }
//        else
//        {
//            imanArb2d.gravityScale = 2;
//        }
//    }


public void CollisionEnable(bool enable)
{
        colForce2d.enabled = enable;
}

public void Print(string data)
    {
        print(data);
    }
}
