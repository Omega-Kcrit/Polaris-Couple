using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImanAController : MonoBehaviour
{
    public PlayerModel playerModel;
    [HideInInspector] public Rigidbody2D rb2D;
    [HideInInspector] public SpriteRenderer spr;
    
    //Colliders de Repulsion
    /*[HideInInspector]*/ public CircleCollider2D colForce2d;

    //GroundPoint
    public Transform groundPoint;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public LayerMask magnetInWorldLayer;

    //IMAN A VALORES
    public bool InControllA = true;
    private ImanAState currentState;
    public bool CollidersEnable = true;

    //IMAN B VALORES
    public Transform imanBtransform;
    public Rigidbody2D imanBrb2d;
    public GameObject imanBObject;
    public Vector3 distanceImanB;

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

    //CHARM-IMPULSO Valores
    public bool skillEnable = false;
    public GameObject SkillIndicator;
    public float ImpulseSkill2 = 10;

    //Pendulo 
    public bool inPendulo = false;
    public float massInPendulo = 1;

    //Animaciones
    public Animator animator;
    public GameObject Prefab_Bolagrande;
    public Vector3 PosPropulsio;
    public float time;

    //public Vector2 positionImanBInImpulse;
    //public Vector3 distanceLimitObjectB;


    // Use this for initialization
    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();        
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("SiguienteBola", time);

        playerModel = Instantiate(playerModel);

        ChangeState(new IASGrounded(this));

        inRepulsion = false;

        repulsionForce = repulsionForceStart;
        velocityInRepulsion = maxVelocityInRepulsion;


    }
    // Update is called once per frame
    void Update ()
    {
        currentState.Update(this);

        distanceImanB = this.transform.position - imanBtransform.position;
        animator.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));
        CollisionEnable(CollidersEnable);

        //print(currentState);
    }
    public void SiguienteBola()
    {
        Instantiate(Prefab_Bolagrande, transform.position + PosPropulsio, Quaternion.identity);
        Invoke("SiguienteBola", time);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);

        //Cooldownd
        if (InControllA)
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
            //ImpulseSkill();            
        }
        if (!InControllA)
        {
            ChangeState(new IASNeutro(this));
        }
    }

    private void LateUpdate()
    {
        currentState.CheckTransition(this);
    }

    public void ChangeState(ImanAState ias)
    {
        currentState = ias;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanBRepulsionArea")
        {
            inRepulsion = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanBRepulsionArea")
        {
            this.rb2D.AddForce(distanceImanB.normalized * repulsionForce, ForceMode2D.Force);
            inRepulsion = true;
        }
        
        //if (collision.gameObject.tag == "ImpulseArea")
        //{
        //    if (Input.GetKey(KeyCode.T))
        //    {
        //        print("skill2");
        //        skillEnable = true;
        //    }
        //    else
        //    {
        //        if (skillEnable)
        //        {
        //            this.rb2D.AddForce((SkillIndicator.transform.up * new Vector2(ImpulseSkill2, ImpulseSkill2)), ForceMode2D.Impulse);
        //        }
        //        skillEnable = false;
        //    }
        //}
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanBRepulsionArea")
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


    public void Print(string data)
    {
        print(data);
    }
    
    public void Charm()
    {
        if (Input.GetKey("e") || InputManager.XButtonGetKey()    /*Input.GetButton("Fire1")*/)
        {
            switchMaxRepulsion = true;
            if (!inRepulsion)
            {
                imanBrb2d.gravityScale = 0;
                imanBrb2d.AddForce(distanceImanB.normalized * forceCharm, ForceMode2D.Force);

                float clampedSpeedX = Mathf.Clamp(imanBrb2d.velocity.x, -speedMaxInCharm, speedMaxInCharm);
                float clampedSpeedY = Mathf.Clamp(imanBrb2d.velocity.y, -speedMaxInCharm, speedMaxInCharm);
                imanBrb2d.velocity = new Vector2(clampedSpeedX, clampedSpeedY);                
            }
            else
            {
                imanBrb2d.gravityScale = 2;
                coolingdownSkill1 = true;
            }
        }
        else
        {
            imanBrb2d.gravityScale = 2;
            velocityInRepulsion = maxVelocityInRepulsion;
            switchMaxRepulsion = false;
        }
        if (Input.GetKeyUp("e") || Input.GetButtonUp("X_Button"))
        {
            coolingdownSkill1 = true;
            switchMaxRepulsion = false;            
        }
    }

   //SI AL FINAL SE USA EL INDICADOR DE SALTO
    //public void ImpulseSkill()
    //{
    //    if (skillEnable)
    //    {
    //        this.ChangeState(new IASNeutro(this));
    //        print("changestate");
    //        rb2D.velocity = new Vector2(0, 0);
    //        rb2D.gravityScale = 0;
    //        SkillIndicator.SetActive(true);
    //        CollisionEnable(false);

    //        imanBObject.GetComponent<ImanBController>().ChangeState(new IBSNeutro(imanBObject.GetComponent<ImanBController>()));
    //        imanBObject.GetComponent<ImanBController>().CollisionEnable(false);
    //        imanBrb2d.GetComponent<Rigidbody2D>().gravityScale = 0;

    //        //Calculo de la distancia
    //        //positionImanBInImpulse = distanceImanB.normalized;
    //        //distanceLimitObjectB = (new Vector2 (this.transform.position.x, this.transform.position.y)) - this.spr.size - this.colExt2d.radius


    //        //positionImanBInImpulse = (this.transform.position + this.spr.size ) + imanBObject.GetComponent<Transform>().transform.position  ;
    //        positionImanBInImpulse = this.transform.position - (new Vector3(2 * distanceImanB.x, 2 * distanceImanB.y, 0));
    //        imanBObject.GetComponent<Transform>().transform.position = Vector2.MoveTowards(new Vector2(imanBObject.GetComponent<Transform>().transform.position.x, imanBObject.GetComponent<Transform>().transform.position.y), positionImanBInImpulse, 1 * Time.deltaTime);

    //        //positionImanBInImpulse = this.colExt2d.GetComponent<CircleCollider2D>().radius;


    //        //if (Input.GetKey("1"))
    //        //{
    //        //    this.transform.rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.rotation;
    //        //    this.transform.localPosition = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.localPosition;
    //        //}
    //        //if (Input.GetKey("2"))
    //        //{
    //        //    this.transform.rotation = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.rotation;
    //        //    this.transform.localPosition = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.localPosition;
    //        //}
    //    }
    //    else
    //    {
    //        //BUG: ESTO HACE QUE A LA HORA DE SALTAR NO SE PUEDA DESPLAZAR DE IZQ/DER EN EL AIRE
    //        //this.ChangeState(new IASGrounded(this));
    //        //rb2D.gravityScale = 2;
    //        //SkillIndicator.SetActive(false);
    //        //CollisionEnable(true);
    //    }
    //}

    public void CollisionEnable(bool enable)
    {
        colForce2d.enabled = enable;
    }

#if UNITY_EDITOR
    public void Depura(string log)
    {
        print(log);
    }
#endif
}
