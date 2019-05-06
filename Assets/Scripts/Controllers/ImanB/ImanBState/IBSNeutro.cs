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
    }

    public override void Update(ImanBController ibc)
    {
    }
}
