using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : RobotBasePart
{

    public float attackDistance = 17f;
    public float startRaycastDistance = 4f;
    private int ANIMATOR_ATTACK_TRIGGER;
    public WashingMachineKettle kettle;
    private int attackDirection = -1;

    new void Start() {
        base.Start();
        ANIMATOR_ATTACK_TRIGGER = Animator.StringToHash("Attack");
    }

    new void Update() {
        base.Update();
        Debug.DrawLine(
            transform.position + attackDirection * transform.right * startRaycastDistance,
            transform.position + attackDirection * transform.right * attackDistance, Color.cyan);
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
        attackDirection = this.robotChunk.GetRobotNum() == 1 ? 1 : -1;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + attackDirection * transform.right * startRaycastDistance, 
            attackDirection * transform.right);
        if (hit.collider != null && hit.distance <= attackDistance)
        {
            Debug.Log(hit.distance);
            Debug.Log(hit.point);
            kettle.gameObject.transform.position = hit.point;
            if (hit.collider.GetComponent<RobotBasePart>() || hit.collider.GetComponentInParent<RobotBasePart>())
            {
                RobotBasePart target = hit.collider.GetComponent<RobotBasePart>() != null ?
                    hit.collider.GetComponent<RobotBasePart>() :
                    hit.collider.GetComponentInParent<RobotBasePart>();
                target.Damage(this.damage);
            }
        }
        else
        {
            kettle.gameObject.transform.localPosition = new Vector3(-attackDistance, 0f, 0f);
        }
        anim.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }
}

