using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASFloatingByEffector : ImanAState
{
    float inputX;
    bool jump = false;

    float cooldownCounter = 0.5f;
    bool coolingdown = false;
    float cooldownCounterStart = 0.5f;

    public bool enableSkill = true;

    //Implementamos un constructor para poder
    //Decidir al inicio respuesta del rigidbody, animaciones...
    public IASFloatingByEffector(ImanAController iac)
    {
        iac.rb2D.isKinematic = false;
        cooldownCounter = cooldownCounterStart;
        //pc.ator.SetBool("Ground", true);
        iac.InControllA = true;
        
    }

    public override void CheckTransition(ImanAController iac)
    {
        //Utilizamos OverlapCircleAll para que nos devuelva todos los
        //colliders encontrados.
        //El problema de esta función es que reserva dinámicamente un array
        //de Collider2D[] cada vez que se la llama, y eso ocupa ciclos de CPU.
        //Una función análoga y más eficiente sería OverlapCircleAllNoAlloc
        Collider2D[] col = Physics2D.OverlapCircleAll(iac.groundPoint.position, iac.playerModel.groundRadius, iac.groundLayer.value + iac.ladderLayer.value + iac.magnetInWorldLayer.value);
        if (col.Length==0)
        {
            iac.ChangeState(new IASOnAir(iac));
        }
        else
        {
            //Recorremos los colliders, la prioridad es (cuestión de diseño de mecánicas):
            //si encuentro una escalera, salgo.
            //podría después, en el caso de que no haya escaleras,
            //seguir recorriendo el array de resultados
            for (int c = 0; c < col.Length; c++)
            {
                //La capa, dentro del gameObject, está almancenada
                // indicando el número de bit que está a uno en su
                //máscara de 32 bits para todas las capas.
                //Así que para comparar con la variable del PC donde
                //tenemos almacenada la capa, es necesario hacer la
                //operacion 2^(número de bit)
                //if ( (1 << col[c].gameObject.layer) == pc.ladderLayer.value)
                //{
                //    if (col[c].gameObject.tag == "TopLadder")
                //        pc.ChangeState(new PSOnTopLadder(pc));
                //    else
                //        pc.ChangeState(new PSOnBottomLadder(pc));

                //    break;
                //}
            }
        }
        if (!iac.InControllA)
        {
            iac.ChangeState(new IASNeutro(iac));
        }
    }

    public override void FixedUpdate(ImanAController iac)
    {
        iac.rb2D.AddForce(Vector2.right * inputX, ForceMode2D.Force);

        float clampedSpeed = Mathf.Clamp(iac.rb2D.velocity.x, -iac.playerModel.speedMax, iac.playerModel.speedMax);
        iac.rb2D.velocity = new Vector2(clampedSpeed, iac.rb2D.velocity.y);  

        if (!coolingdown)
        {
            if (jump)
            {
                iac.rb2D.AddForce(Vector2.up * iac.playerModel.jumpImpulse, ForceMode2D.Impulse);
                Debug.Log("jumpin");
            }            
            cooldownCounter = cooldownCounterStart;
            coolingdown = true;
        }
        else
        {
            cooldownCounter -= Time.deltaTime;            
                if (cooldownCounter <= 0)
                {
                    coolingdown = false;
                }            
        }
    }

    public override void Update(ImanAController iac)
    {
        //inputX = Input.GetAxis("Horizontal") * iac.playerModel.horizontalForce;

        //jump = Input.GetAxis("Jump");

        inputX = Input.GetAxis("J_MainHorizontal") * iac.playerModel.horizontalForce;

        if (Input.GetButton("Horizontal")) inputX = Input.GetAxis("Horizontal") * iac.playerModel.horizontalForce;

        //if (InputManager.AButton()) jump = true;
        jump = InputManager.AButton() || Input.GetButtonDown("Jump");

        //pc.ator.SetFloat("Speed", Mathf.Abs(pc.rb2D.velocity.x));

        if (iac.rb2D.velocity.x > 0)
            iac.spr.flipX = false;
        if (iac.rb2D.velocity.x < 0)
            iac.spr.flipX = true;
    }
}
