using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBasePart : MonoBehaviour
{
    public enum State { DETTACHED, ATTACHED};
    private State curr_state;
    private Rigidbody2D RB;

    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        health = 4;
        RB = transform.GetComponent<Rigidbody2D>();
        //GameObject.Destroy(RB);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // switch between attached to dettached
    public void ChangeState() {
        curr_state = (curr_state == State.DETTACHED) ?  State.ATTACHED : State.DETTACHED;
    }

    // return true if the robot part got picked already
    public bool IsPicked() {
        return curr_state == State.ATTACHED;
    }
    public void ChangeState(State state)
    {
        curr_state = state;
        if (state == State.ATTACHED)
        {
            Debug.Log(" pickingup");
            if (RB != null)
            {
                Debug.Log("there RB got value");
                GameObject.Destroy(RB);
            }
            Debug.Log("RB is null ");

        }
        else if (RB == null && state != State.ATTACHED) { gameObject.AddComponent<Rigidbody2D>(); }
    }
    public State GetState()
    {
        return curr_state;
    }

    public void Damage ()
    {
        health -= 1;
        if (health < 0) {
            PrintInDeadZone();
            // todo : to call the function which drop the robot_part on the floor.
        }
    }
    public void Recharge() {
        health += 1;
    }

    public void PrintInDeadZone () {
        Debug.Log("this is a dead zone. tnx next");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // cheack if the object which was colliosion with is also an robot part/
        if (collision.gameObject.name == "RobotBasePart")
        {
            this.Damage();
            Debug.Log("now this item got " + this.health + " number of life");
        }
    }
}
