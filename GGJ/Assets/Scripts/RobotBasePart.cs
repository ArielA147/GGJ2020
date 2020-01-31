﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBasePart : MonoBehaviour
{
    public enum State { DETTACHED, ATTACHED };
    private State curr_state;
    private Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField]
    private int health = 10;
    public int recharge_unit = 1;
    public int drop_health = 1;
    public RobotChunk robotChunk;
    public RobotChunk potentialRobotChunk;


    public int Health
    {
        get { return health; }
        set {
            // Can't have less health than drop health
            health = Mathf.Max(value, drop_health);
            if (health == drop_health) {
                Drop();
            }
        }
    }

    public State GetState() { return curr_state; }


    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // switch between attached to dettached
    public void ToggleAttached() {
        curr_state = (curr_state == State.DETTACHED) ?  State.ATTACHED : State.DETTACHED;
    }

    // return true if the robot part got picked already
    public bool IsPicked() {
        return curr_state == State.ATTACHED;
    }

    public void Damage (int damage) { health -= damage; }

    public void Recharge() { health += recharge_unit; }

    public void Drop() {
        if (IsInRobotArea())
        {
            Debug.Log("in robot area");
            AttachToRobot();
        }
        else {
            FallDown();
        }
    }

    private void AttachToRobot() {
        AttachTo(potentialRobotChunk.transform);
        this.robotChunk = this.potentialRobotChunk;
        this.potentialRobotChunk = null;
        this.curr_state = State.ATTACHED;
    }


    bool IsInRobotArea() {

        Debug.Log("potentialRobotChunk - ", this.potentialRobotChunk);
        Debug.Log("robotChunk - ", this.robotChunk);
        return this.potentialRobotChunk != null &&  this.robotChunk == null;
    }

    private void FallDown() {
        this.transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        curr_state = State.DETTACHED;
    }

    public void AttachTo(Transform newParent)
    {
        Debug.Log("Attaching Robot part");
        rb.bodyType = RigidbodyType2D.Kinematic;
        this.transform.parent = newParent;
        curr_state = State.ATTACHED;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // cheack if the object which was colliosion with is also an robot part/
        if (collision.gameObject.name == "RobotBasePart")
        {
            this.Damage(1);
            //Debug.Log("Now this item has " + this.health + " life");
        }
    }


}
