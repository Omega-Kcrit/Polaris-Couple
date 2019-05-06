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

    //Charm Valores   
    public float speedMaxInCharm = 1;
    public float forceCharm = 20f;

    //Fede: Cooldown para la habilidad de atraer
    public bool coolingdownSkill1 = false;
    public float cooldownCounterSkill1 = 3f;
    public float cooldownCounterStartSkill1 = 3f;


    //Pendulo 
    public bool inPendulo = false;
    public float massInPendulo = 1;

    //Animaciones
    public Animator animator;
    public GameObject Prefab_Bolagrande;
    public Vector3 PosPropulsio;
    public float time;

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
        CollisionEnable(CollidersEnable);
        
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
            this.rb2D.AddForce(distanceImanA.normalized * repulsionForce, ForceMode2D.Force);
            inRepulsion = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanARepulsionArea")
        {
            if (switchMaxRepulsion)
            {
                velocityInRepulsion = maxVelocityInImpulse;
                repulsionForce = repulsionForceInCharm;
                float clampedSpeedX = Mathf.Clamp(this.rb2D.velocity.x, -velocityInRepulsion, velocityInRepulsion);
                float clampedSpeedY = Mathf.Clamp(this.rb2D.velocity.y, -velocityInRepulsion, velocityInRepulsion);
                this.rb2D.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
            }
            else
            {
                velocityInRepulsion = maxVelocityInRepulsion;
                repulsionForce = repulsionForceStart;
                float clampedSpeedX = Mathf.Clamp(this.rb2D.velocity.x, -velocityInRepulsion, velocityInRepulsion);
                float clampedSpeedY = Mathf.Clamp(this.rb2D.velocity.y, -velocityInRepulsion, velocityInRepulsion);
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
    }
    

public void Charm()
{
    if (Input.GetKey("e") || InputManager.XButtonGetKey())
    {
        switchMaxRepulsion = true;
        if (!inRepulsion)
        {
            imanArb2d.gravityScale = 0;
            imanArb2d.AddForce(distanceImanA.normalized * forceCharm, ForceMode2D.Force);

            float clampedSpeedX = Mathf.Clamp(imanArb2d.velocity.x, -speedMaxInCharm, speedMaxInCharm);
            float clampedSpeedY = Mathf.Clamp(imanArb2d.velocity.y, -speedMaxInCharm, speedMaxInCharm);
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
        coolingdownSkill1 = true;
        switchMaxRepulsion = false;
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
