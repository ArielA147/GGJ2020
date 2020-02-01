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
        //Debug.DrawLine(
        //    transform.position + attackDirection * transform.right * startRaycastDistance,
        //    transform.position + attackDirection * transform.right * attackDistance, Color.cyan);
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
        bool isPlayer1 = this.robotChunk.GetRobotNum() == 1;
        attackDirection = isPlayer1 ? 1 : -1;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + attackDirection * transform.right * startRaycastDistance,
            attackDirection * transform.right, attackDistance);
        int hit_index = -1;
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];
            bool isChildOfRobotPart = hit.collider.GetComponent<RobotBasePart>() || hit.collider.GetComponentInParent<RobotBasePart>();
            if (isChildOfRobotPart) {
                RobotBasePart target = hit.collider.GetComponent<RobotBasePart>() != null ?
                    hit.collider.GetComponent<RobotBasePart>() :
                    hit.collider.GetComponentInParent<RobotBasePart>();
                bool isEnemyRobot = target.robotChunk != null && target.robotChunk.GetRobotNum() != this.robotChunk.GetRobotNum();
                if (isEnemyRobot) {
                    hit_index = i;
                    break;
                }
            }
        }

        if (hit_index != -1)
        {
            RaycastHit2D hit = hits[hit_index];
            kettle.gameObject.transform.position = hit.point;
            if (hit.collider.GetComponent<RobotBasePart>() || hit.collider.GetComponentInParent<RobotBasePart>())
            {
                RobotBasePart target = hit.collider.GetComponent<RobotBasePart>() != null ?
                    hit.collider.GetComponent<RobotBasePart>() :
                    hit.collider.GetComponentInParent<RobotBasePart>();
                target.Damage(this.damage, 0.5f);
            }
        }
        else
        {
            kettle.gameObject.transform.localPosition = new Vector3(-attackDistance, 0f, 0f);
        }
        anim.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }
}

