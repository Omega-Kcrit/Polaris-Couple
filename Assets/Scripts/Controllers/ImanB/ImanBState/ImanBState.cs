using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImanBState
{
    public abstract void Update(ImanBController ibc);
    public abstract void FixedUpdate(ImanBController ibc);
    public abstract void CheckTransition(ImanBController ibc);
}
