using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float __velocity;
    public float jumpSpeed = 1f;
    private Rigidbody2D RB; // RB for the player
    public Robot myRobot; // the robot of the palyer
    private RobotBasePart pickup; // the robot part that is curently hold by the player
    private RobotBasePart potentialPart;
    public int playerNum;
    public Animator anim;

    private KeyCode player_left, player_right, player_lift, player_jump;



    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        myRobot = GameObject.FindObjectOfType<Robot>();
        SetPlayerKeys(playerNum);
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        Controller();
        gameObject.name = "RobotBasePart";
        if (Input.GetKeyDown("m"))
        {
            ActionButton();
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
           // Debug.Log("jump");
            RB.velocity = new Vector2( RB.velocity.x, jumpSpeed);
        }
    }

    private void ActionButton() {
        if (pickup != null) {
            DropRobotPart();
        } else if (potentialPart != null) { 
            PickupRobotPart();
        }
    }

    // the player picking an object and hold him
    private void PickupRobotPart() {
        // check if the part is avalible
        if (CanPickUp()) { // the part is free to pickup
            potentialPart.AttachTo(transform);
            this.pickup = potentialPart;
            // moving the part to the player pos. 
            // the y pos of the part will be bigger than the palyer pos ("over the shoulder").
            pickup.transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Abs(GetComponent<Collider2D>().bounds.max.y));
            pickup.transform.SetParent(gameObject.transform); // moving the part to be the sun of the palyer 
        }
    }


    private bool CanPickUp() {
        return 
            this.potentialPart != null &&
            this.potentialPart.GetState() == RobotBasePart.State.DETTACHED &&
            this.pickup == null;
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


    private void DropRobotPart()
    {
        if (pickup == null)
        {
            Debug.Log("trying to drop a part when non is available");
            return;
        }

        pickup.Drop();
        pickup = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Interaction")) {
            
            this.potentialPart = collision.GetComponentInParent<RobotBasePart>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Interaction") &&
            potentialPart != null && 
            GameObject.ReferenceEquals(collision.GetComponentInParent<RobotBasePart>().gameObject, potentialPart.gameObject))
        {
            this.potentialPart = null;
        }
    }
}
