using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float __velocity;
    public float jumpSpeed = 1f;
    private Rigidbody2D RB; // RB for the player
    public robot myRobot; // the robot of the palyer
    private bool is_lifting = false; // is the player can lift someting right now
    private RobotBasePart curr_part; // the robot part that is curently hold by the player


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
                curr_part.transform.SetParent(myRobot.transform);
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

    }

    private void DropingRobotPart(RobotBasePart part) {
        part.Drop();
        part.transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Abs(GetComponent<Collider2D>().bounds.max.y));
        part.transform.SetParent(gameObject.transform); // todo : to check where the dropping occuried 
        is_lifting = true;
        curr_part = part;
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
                    part.transform.SetParent(myRobot.transform);

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

                curr_part = part;
               
            }        
        }
    }
 }
