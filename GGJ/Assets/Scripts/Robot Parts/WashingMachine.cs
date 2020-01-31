using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : RobotBasePart
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual bool HasAttack()
    {
        return true;
    }

    protected virtual float GetAttackIntervalInSeconds()
    {
        return 5f; //not relevant for base
    }

    protected virtual void Attack()
    {
        Debug.Log("Punching");
    }
}

