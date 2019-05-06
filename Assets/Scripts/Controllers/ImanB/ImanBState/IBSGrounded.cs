using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBSGrounded : ImanBState
{
    float inputX;
    bool jump = false;

    float cooldownCounter = 0.01f;
    bool coolingdown = false;
    float cooldownCounterStart = 0.01f;

    //Implementamos un constructor para poder
    //Decidir al inicio respuesta del rigidbody, animaciones...
    public IBSGrounded(ImanBController ibc)
    {
        ibc.rb2D.isKinematic = false;
        cooldownCounter = cooldownCounterStart;
        //pc.ator.SetBool("Ground", true);
        ibc.InControllB = true;
    }

    public override void CheckTransition(ImanBController ibc)
    {
        //Utilizamos OverlapCircleAll para que nos devuelva todos los
        //colliders encontrados.
        //El problema de esta función es que reserva dinámicamente un array
        //de Collider2D[] cada vez que se la llama, y eso ocupa ciclos de CPU.
        //Una función análoga y más eficiente sería OverlapCircleAllNoAlloc
        Collider2D[] col = Physics2D.OverlapCircleAll(ibc.groundPoint.position, ibc.playerModel.groundRadius, ibc.groundLayer.value + ibc.ladderLayer.value);
        if (col.Length==0)
        {
            ibc.ChangeState(new IBSOnAir(ibc));
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

        if (!ibc.InControllB)
        {
            ibc.ChangeState(new IBSNeutro(ibc));
        }
    }

    public override void FixedUpdate(ImanBController ibc)
    {
        ibc.rb2D.AddForce(Vector2.right * inputX, ForceMode2D.Force);

        float clampedSpeed = Mathf.Clamp(ibc.rb2D.velocity.x, -ibc.playerModel.speedMax, ibc.playerModel.speedMax);
        ibc.rb2D.velocity = new Vector2(clampedSpeed, ibc.rb2D.velocity.y);  

        if (!coolingdown)
        {
            if (jump)
            {
                ibc.rb2D.AddForce(Vector2.up * ibc.playerModel.jumpImpulse, ForceMode2D.Impulse);
            }            
            cooldownCounter = cooldownCounterStart;
            coolingdown = true;
        }
        else
        {
            cooldownCounter -= Time.deltaTime;
            {
                if (cooldownCounter <= 0)
                {
                    coolingdown = false;
                }
            }
        }
    }

    public override void Update(ImanBController ibc)
    {
        //inputX = Input.GetAxis("Horizontal") * ibc.playerModel.horizontalForce;

        //jump = Input.GetAxis("Jump");

        inputX = InputManager.MainHorizontal() * ibc.playerModel.horizontalForce;

        if (Input.GetButton("Horizontal")) inputX = Input.GetAxis("Horizontal") * ibc.playerModel.horizontalForce;

        //if (InputManager.AButton()) jump = true;
        jump = InputManager.AButton() || Input.GetButton("Jump");

        //pc.ator.SetFloat("Speed", Mathf.Abs(pc.rb2D.velocity.x));

        if (ibc.rb2D.velocity.x > 0)
            ibc.spr.flipX = false;
        if (ibc.rb2D.velocity.x < 0)
            ibc.spr.flipX = true;
    }
}
