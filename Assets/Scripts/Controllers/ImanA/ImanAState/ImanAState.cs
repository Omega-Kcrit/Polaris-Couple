using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImanAState
{
    public abstract void Update(ImanAController iac);
    public abstract void FixedUpdate(ImanAController iac);
    public abstract void CheckTransition(ImanAController iac);
}
