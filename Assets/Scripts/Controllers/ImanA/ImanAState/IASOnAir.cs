using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASOnAir : ImanAState
{
    float inputX;
    bool jump;
    float numCurrentAirJumping;

    //Implementamos un constructor para poder
    //Decidir al inicio respuesta del rigidbody, animaciones...
    public IASOnAir(ImanAController iac)
    {
        iac.rb2D.isKinematic = false;
        //pc.ator.SetBool("Ground", false);
    }

    public override void CheckTransition(ImanAController iac)
    {
        Collider2D colMagnetsInWorld = Physics2D.OverlapCircle(iac.groundPoint.position, iac.playerModel.groundRadius, iac.magnetInWorldLayer.value);
        if (colMagnetsInWorld)
        {
            iac.ChangeState(new IASFloatingByEffector(iac));
            Debug.Log("Entering Floating by effector");
        }

        Collider2D col = Physics2D.OverlapCircle(iac.groundPoint.position, iac.playerModel.groundRadius, iac.groundLayer.value);
        if (col)
        {
            numCurrentAirJumping = 0;
            iac.ChangeState(new IASGrounded(iac));
        }

        if (!iac.InControllA)
        {
            iac.ChangeState(new IASNeutro(iac));
        }
    }

    public override void FixedUpdate(ImanAController iac)
    {
        iac.rb2D.AddForce(Vector2.right * inputX, ForceMode2D.Force);

        if (!iac.inPendulo)
        {
            float clampedSpeed = Mathf.Clamp(iac.rb2D.velocity.x, -iac.playerModel.speedMax, iac.playerModel.speedMax);
            iac.rb2D.velocity = new Vector2(clampedSpeed, iac.rb2D.velocity.y);
            iac.rb2D.mass = 1;
        }
        else
        {
            iac.rb2D.mass = iac.massInPendulo;
        }
        


    }

    public override void Update(ImanAController iac)
    {
        //inputX = Input.GetAxis("Horizontal") * iac.playerModel.horizontalForce * iac.playerModel.jumpSpeedFactor;
        inputX = Input.GetAxis("J_MainHorizontal") * iac.playerModel.horizontalForce * iac.playerModel.jumpSpeedFactor;
        if (Input.GetButton("Horizontal")) inputX = Input.GetAxis("Horizontal") * iac.playerModel.horizontalForce;

        //jump = Input.GetButtonDown("Jump") || InputManager.AButton();
        
        if (iac.rb2D.velocity.x > 0)
            iac.spr.flipX = false;
        if (iac.rb2D.velocity.x < 0)
            iac.spr.flipX = true;
    }
}
