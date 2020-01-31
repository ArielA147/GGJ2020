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
    public int playerNum;
    public Animator anim;

    private KeyCode player_left, player_right, player_lift, player_jump;
    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        SetPlayerKeys(playerNum);
        anim = GetComponent<Animator>();
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
        if (Input.GetKey("left"))
        {
            RB.velocity = new Vector2(-1f * __velocity, RB.velocity.y);
           
            anim.SetBool("is_running", true);
        }
        else if (Input.GetKey("right"))
        {
            RB.velocity = new Vector2(1f*__velocity, RB.velocity.y);
            anim.SetBool("is_running", true);
        }
        else
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
            anim.SetBool("is_running", false);
        }
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("jump");
            RB.velocity = new Vector2( RB.velocity.x, 20f);
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

    private void SetPlayerKeys(int player_num)
    {
        switch (player_num)
        {
            case 1:
                player_left = KeyCode.LeftArrow;
                player_right = KeyCode.RightArrow;
                player_lift = KeyCode.M;
                player_jump = KeyCode.Space;
                break;
            case 2:
                player_left = KeyCode.A;
                player_right = KeyCode.D;
                player_lift = KeyCode.W;
                player_jump = KeyCode.F;
                break;

        }
       
    }


}
