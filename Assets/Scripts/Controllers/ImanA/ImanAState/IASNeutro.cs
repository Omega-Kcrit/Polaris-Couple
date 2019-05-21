using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASNeutro : ImanAState
{
    public IASNeutro (ImanAController iac)
    {

    }

    public override void CheckTransition(ImanAController iac)
    {
        if (iac.InControllA)
        {            
            iac.ChangeState(new IASGrounded(iac));
        }
    }

    public override void FixedUpdate(ImanAController iac)
    {
        float calmpedSpeed = Mathf.Clamp(iac.rb2D.velocity.x, -iac.playerModel.speedMaxInNeutro, iac.playerModel.speedMaxInNeutro);
        iac.rb2D.velocity = new Vector2(calmpedSpeed, iac.rb2D.velocity.y);

    }

    public override void Update(ImanAController iac)
    {
        if (iac.rb2D.velocity.y > 0)
        {
            iac.OnAir = true;
        }
        else
        {
            iac.OnAir = false;
        }
    }
}
