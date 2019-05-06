using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBSOnAir : ImanBState
{
    float inputX;
    bool jump;
    float numCurrentAirJumping;

    //Implementamos un constructor para poder
    //Decidir al inicio respuesta del rigidbody, animaciones...
    public IBSOnAir(ImanBController ibc)
    {
        ibc.rb2D.isKinematic = false;
        //pc.ator.SetBool("Ground", false);
    }

    public override void CheckTransition(ImanBController ibc)
    {
        Collider2D col = Physics2D.OverlapCircle(ibc.groundPoint.position, ibc.playerModel.groundRadius, ibc.groundLayer.value);
        if (col)
        {
            numCurrentAirJumping = 0;
            ibc.ChangeState(new IBSGrounded(ibc));
        }

        if (!ibc.InControllB)
        {
            ibc.ChangeState(new IBSNeutro(ibc));
        }
    }

    public override void FixedUpdate(ImanBController ibc)
    {
        ibc.rb2D.AddForce(Vector2.right * inputX, ForceMode2D.Force);

        if (!ibc.inPendulo)
        {
            float clampedSpeed = Mathf.Clamp(ibc.rb2D.velocity.x, -ibc.playerModel.speedMax, ibc.playerModel.speedMax);
            ibc.rb2D.velocity = new Vector2(clampedSpeed, ibc.rb2D.velocity.y);
            ibc.rb2D.mass = 1;
        }
        else
        {
            ibc.rb2D.mass = ibc.massInPendulo;
        }

    }

    public override void Update(ImanBController ibc)
    {
        //inputX = Input.GetAxis("Horizontal") * ibc.playerModel.horizontalForce * ibc.playerModel.jumpSpeedFactor;
        inputX = Input.GetAxis("J_MainHorizontal") * ibc.playerModel.horizontalForce * ibc.playerModel.jumpSpeedFactor;
        if (Input.GetButton("Horizontal")) inputX = Input.GetAxis("Horizontal") * ibc.playerModel.horizontalForce;

        //jump = Input.GetButtonDown("Jump") || InputManager.AButton();

        if (ibc.rb2D.velocity.x > 0)
            ibc.spr.flipX = false;
        if (ibc.rb2D.velocity.x < 0)
            ibc.spr.flipX = true;
    }
}
