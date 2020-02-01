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
    private bool is_jumping = false;
    public KeyCode player_left, player_right, player_lift, player_jump, player_rotate, player_fix;
    private bool isRight;
    public Vector2 holdPosition = new Vector2(-0.5f, 2.3f);
    SpriteRenderer sr;
    public float maxRotationAngle = 30f;
    private int rotation;
        public float rechargeInterval = 1f;
    private float timeSinceLastRechargeAction = 0f;
    [Range(1,2)]
    public int player_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        isRight = playerNum == 1;
        sr = GetComponent<SpriteRenderer>();
        RB = transform.GetComponent<Rigidbody2D>();
        myRobot = GameObject.FindObjectOfType<Robot>();
        SetPlayerKeys(playerNum);
        anim = GetComponent<Animator>();
        SetPlayerKeys(playerNum);
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        HandlePlayerActions();
    }

    private void HandlePlayerActions() {
        if (Input.GetKeyDown(player_lift))
        {
            ActionButton();
        }
        if (pickup != null && Input.GetKey(player_rotate))
        {
            this.rotation = myRobot.robotNumber * 90;
            int rot_direction = pickup.transform.rotation.eulerAngles.z > maxRotationAngle ? 1 : -1;
            pickup.transform.Rotate(new Vector3(0, 0, 2));
        }
        if (Input.GetKey(player_fix) && CanFix())
        {
            //TODO: this will recharge too fast :(
            this.potentialPart.Recharge();
        }
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(player_left))
        {
            RB.velocity = new Vector2(-1f * __velocity, RB.velocity.y);
            isRight = false;
            //anim.SetBool("is_running", true);
        }
        else if (Input.GetKey(player_right))
        {
            isRight = true;
            RB.velocity = new Vector2(1f*__velocity, RB.velocity.y);
            //anim.SetBool("is_running", true);
        }
        else
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
            //anim.SetBool("is_running", false);
        }
        sr.flipX = isRight;

        if (Input.GetKeyDown(player_jump) && !is_jumping)
        {
            is_jumping = true;
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
            pickup.transform.localPosition = holdPosition;
            if ((player_num == 1 && pickup.transform.localScale.x > 0) ||
                (player_num == 2 && pickup.transform.localScale.x < 0)) {
                pickup.Flip();
            }
        }
    }


    private bool CanPickUp() {
        return 
            this.potentialPart != null &&
            this.potentialPart.GetState() == RobotBasePart.State.DETTACHED &&
            this.pickup == null;
    }

    private bool CanFix() { return this.potentialPart != null && this.pickup == null; }


    private void SetPlayerKeys(int player_num) 
    {
        switch (player_num)
        { 
            case 1:
                player_left = KeyCode.LeftArrow;
                player_right = KeyCode.RightArrow;
                player_jump = KeyCode.UpArrow;
                player_lift = KeyCode.K;
                player_rotate = KeyCode.DownArrow;
                player_fix = KeyCode.L;
                break;
            case 2:
                player_left = KeyCode.A;
                player_right = KeyCode.D;
                player_jump = KeyCode.W;
                player_lift = KeyCode.V;
                player_rotate = KeyCode.S;
                player_fix = KeyCode.B;
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

        pickup.Drop(player_num);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        is_jumping = false;
    }
}
