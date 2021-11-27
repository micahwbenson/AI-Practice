using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ears : Senses
{
    protected override bool HasDetected(Detectable detectable)
    {
        //Doing a distance check between the players detectable component and this -- Does this when within Distance and CanBeHear is set -- These variables are pulled from the parent class Senses
        return Vector2.Distance(detectable.transform.position, transform.position) <= Distance && detectable.CanBeHear;
    }
}
