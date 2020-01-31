using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool canPickUp = true;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public float __velocity;
    public float jumpSpeed = 1f;
    private Rigidbody2D RB; // RB for the player
    private bool is_lifting = false;
    public robot robot; // the robot of the palyer
    private RobotBasePart pickup; // the robot part that is curently hold by the player


    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        robot myRobot = GameObject.FindObjectOfType<robot>();
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
        gameObject.name = "RobotBasePart";
        if (Input.GetKeyDown("m"))
        {
            if (is_lifting)
            {
                pickup.transform.SetParent(robot.transform);
            }
        }
    }

    private void Controller()
    {
        if (Input.GetButton("Horizontal"))
        {
            RB.velocity = new Vector2(Input.GetAxis("Horizontal")*__velocity, RB.velocity.y);
        }
        else
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
        }
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("jump");
            RB.velocity = new Vector2( RB.velocity.x, jumpSpeed);
        }
    }


    // the player picking an object and hold him
    private void PickingRobotPart(RobotBasePart part) {
        // check if the part is avalible
        if (part.GetState().ToString().Equals("DETTACHED")) { // the part is free to pickup
            if (canPickUp) // check if player can pickup a part
            {
                part.ChangeState(); // change the part status - to detach
                // moving the part to the player pos. 
                // the y pos of the part will be bigger than the palyer pos ("over the shoulder").
                part.transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Abs(GetComponent<Collider2D>().bounds.max.y));
                part.transform.SetParent(gameObject.transform); // moving the part to be the sun of the palyer 
                ChangePickpuMod();
                // change the part to be a son of the palyer in th TREEGAME
                part.transform.SetParent(gameObject.transform);
            }
            else DropingRobotPart(part);
        }

        
    }

    private void ChangePickpuMod() {
        canPickUp = !canPickUp;
    }

    private void DropingRobotPart(RobotBasePart part) {
        part.ChangeState();

        // check where the robotpart is ? - on the robot or other place.
        // if on robot -> the rp going as a son of the robot in this position.
        // else -> the rp is falling down, 

         is_lifting = true;
        pickup = part;
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "RobotBasePart" && Input.GetKeyDown("m"))
        {
            // check if the user holding something
            // if holding something : drop the object
            // if no holding something : 
            // check if the part is availble
            // if true - pickup , change the state of the part for attacted 
            // else do nothing

            RobotBasePart part = collision.gameObject.GetComponent<RobotBasePart>();
            Debug.Log(part.GetState() + ", " + is_lifting);
            if (part.GetState() == RobotBasePart.State.ATTACHED)
            { 
                if (is_lifting)
                {
                    part.transform.SetParent(robot.transform);

                }
                else
                {

                }
            }
            else if (part.GetState() == RobotBasePart.State.DETTACHED )
            {
                part.ChangeState(RobotBasePart.State.ATTACHED);
                part.transform.position = new Vector2(transform.position.x, transform.position.y +Mathf.Abs( GetComponent<Collider2D>().bounds.max.y));
                part.transform.SetParent(gameObject.transform); // changing the parent of the part to this object
                is_lifting = true;

                pickup = part;
               
            }        
        }
    }
 }
