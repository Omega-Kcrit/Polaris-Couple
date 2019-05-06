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
    }

    public override void Update(ImanAController iac)
    {
    }
}
