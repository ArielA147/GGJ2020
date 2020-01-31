using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : RobotBasePart
{

    public float attackDistance = 15f;
    public float startRaycastDistance = 4f;
    private int ANIMATOR_ATTACK_TRIGGER;
    public WashingMachineKettle kettle;

    new void Start() {
        base.Start();
        //kettle = GetComponentInChildren<WashingMachineKettle>();
        ANIMATOR_ATTACK_TRIGGER = Animator.StringToHash("Attack");
    }

    new void Update() {
        base.Update();
        Debug.DrawRay(
            transform.position - transform.right * startRaycastDistance, 
            -transform.right, Color.cyan);
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
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position - transform.right * startRaycastDistance, -transform.right);
        if (hit.collider != null)
        {
            kettle.gameObject.transform.position = hit.point;
        }
        anim.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }
}

