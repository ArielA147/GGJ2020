using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : RobotBasePart
{

    private int ANIMATOR_ATTACK_TRIGGER;
    public Transform selfFloorIndicator;
    public Transform worldFloor;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        ANIMATOR_ATTACK_TRIGGER = Animator.StringToHash("zap");
    }

    protected override bool HasAttack()
    {
        return true;
    }

    public override bool CanRotate()
    {
        return false;
    }


    protected override float GetAttackIntervalInSeconds()
    {
        return 12f; //not relevant for base
    }

    protected override void Attack()
    {
        selfFloorIndicator.position = worldFloor.position;
        anim.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
        RobotBasePart[] allParts = FindObjectsOfType<RobotBasePart>();
        foreach (RobotBasePart part in allParts) {
            if (part.robotChunk != null && part.robotChunk.GetRobotNum() != this.robotChunk.GetRobotNum()) {
                part.Damage(damage);
            }
        }
    }


}