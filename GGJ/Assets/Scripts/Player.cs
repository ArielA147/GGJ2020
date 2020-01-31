using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float __velocity;
    private Rigidbody2D RB;
    public robot myRobot;
    private bool is_lifting = false;
    private robot_part curr_part;
    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        //robot myRobot = GameObject.FindObjectOfType<robot>();


    }

    // Update is called once per frame
    void Update()
    {
        Controller();
        gameObject.name = "robot_part";
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
            RB.velocity = new Vector2( RB.velocity.x, 6f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "robot_part" && Input.GetKeyDown("m"))
        {
            robot_part part = collision.gameObject.GetComponent<robot_part>();
            Debug.Log(part.GetState() + ", " + is_lifting);
            if (part.GetState() == robot_part.State.ATTACHED)
            { 
                
                if (is_lifting)
                {
                    part.transform.SetParent(myRobot.transform);
                    
                }
                else
                {

                }
            }
            else if (part.GetState() == robot_part.State.DETTACHED )
            {
                part.ChangeState(robot_part.State.ATTACHED);
                part.transform.position = new Vector2(transform.position.x, transform.position.y +Mathf.Abs( GetComponent<Collider2D>().bounds.max.y));
                part.transform.SetParent(gameObject.transform);
                is_lifting = true;

                curr_part = part;
               
            }        
        }
    }
 }
