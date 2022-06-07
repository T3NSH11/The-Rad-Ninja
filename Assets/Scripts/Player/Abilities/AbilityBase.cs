using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase
{
    public abstract void Activation(AbilityMain Main);

    public abstract void Action(AbilityMain Main);
}
