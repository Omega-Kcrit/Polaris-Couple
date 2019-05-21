using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBSNeutro : ImanBState
{
    public IBSNeutro(ImanBController ibc)
    {

    }


    public override void CheckTransition(ImanBController ibc)
    {
        if (ibc.InControllB)
        {
            ibc.ChangeState(new IBSGrounded(ibc));
        }
    }

    public override void FixedUpdate(ImanBController ibc)
    {
        float calmpedSpeed = Mathf.Clamp(ibc.rb2D.velocity.x, -ibc.playerModel.speedMaxInNeutro, ibc.playerModel.speedMaxInNeutro);
        ibc.rb2D.velocity = new Vector2(calmpedSpeed, ibc.rb2D.velocity.y);
    }

    public override void Update(ImanBController ibc)
    {

        if (ibc.rb2D.velocity.y > 0)
        {
            ibc.OnAir = true;
        }
        else
        {
            ibc.OnAir = false;
        }
    }
}
