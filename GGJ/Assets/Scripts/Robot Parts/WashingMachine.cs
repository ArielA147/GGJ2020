using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : RobotBasePart
{
    protected override bool HasAttack()
    {
        return true;
    }

    protected override float GetAttackIntervalInSeconds()
    {
        return 5f; //not relevant for base
    }

    protected override void Attack()
    {
        Debug.Log("Punching");
    }
}

