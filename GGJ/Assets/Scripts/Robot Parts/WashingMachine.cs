using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : RobotBasePart
{

    private int ANIMATOR_ATTACK_TRIGGER;

    new void Start() {
        base.Start();
        ANIMATOR_ATTACK_TRIGGER = Animator.StringToHash("Attack");
    }

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
        anim.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }
}

