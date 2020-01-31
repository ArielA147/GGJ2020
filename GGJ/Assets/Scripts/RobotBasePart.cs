using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBasePart : MonoBehaviour
{
    public enum State { DETTACHED, ATTACHED };
    private State curr_state;
    public Rigidbody2D rb;
    [SerializeField]
    private int health = 10;
    public int recharge_unit = 1;
    public int drop_health = 1;
    public RobotChunk robotChunk;
    public RobotChunk potentialRobotChunk;
    private bool isAttacking = false;


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
        Debug.Log("Starting Robot Part");
        rb = transform.GetComponent<Rigidbody2D>();
        isAttacking = robotChunk != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack() && !isAttacking) {
            StartAttacking();
        }
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
        CancelInvoke("Attack");
        isAttacking = false;
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

    private bool CanAttack() {
        return robotChunk != null && HasAttack();
    }

    private void StartAttacking() {
        if (isAttacking) {
            Debug.Log("Trying to start attack cycle when already attacking");
            return; 
        }
        isAttacking = true;
        InvokeRepeating("Attack", 0f, GetAttackIntervalInSeconds());
    }

    protected virtual bool HasAttack() {
        return false;
    }

    protected virtual float GetAttackIntervalInSeconds() {
        return -1f; //not relevant for base
    }

    protected virtual void Attack() {
        //do nothing
    }


}
