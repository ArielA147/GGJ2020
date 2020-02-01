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
        ANIMATOR_ATTACK_TRIGGER = Animator.StringToHash("Attack");
    }

    new void Update() {
        base.Update();
        Debug.DrawLine(
            transform.position - transform.right * startRaycastDistance,
            transform.position - transform.right * attackDistance, Color.cyan);
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
            transform.position - transform.right * startRaycastDistance, -transform.right * this.transform.localScale.x);
        if (hit.collider != null && hit.distance <= attackDistance)
        {
            Debug.Log(hit.distance);
            kettle.gameObject.transform.position = hit.point;
            if (hit.collider.GetComponent<RobotBasePart>() || hit.collider.GetComponentInParent<RobotBasePart>()) {
                RobotBasePart target = hit.collider.GetComponent<RobotBasePart>() != null ?
                    hit.collider.GetComponent<RobotBasePart>() : 
                    hit.collider.GetComponentInParent<RobotBasePart>();
                target.Damage(this.damage);
            }
        }
        kettle.gameObject.transform.localPosition = new Vector3(-attackDistance, 0f, 0f);
        anim.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }
}

