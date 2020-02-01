using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float __velocity;
    public float jumpSpeed = 1f;
    private Rigidbody2D RB; // RB for the player
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
    public float rechargeInterval = 1f;
    [Range(1,2)]
    public int player_num = 0;
    string[] collidableLayers = { "Ground", "Pltaform" };
    Transform graphicsRootTransform;

    int ANIM_GROUNDED_BOOL, ANIM_JUMP_TRIGGER, ANIM_RUN_BOOL, ANIM_FIXING_BOOL;


    // Start is called before the first frame update
    void Start()
    {
        ANIM_GROUNDED_BOOL = Animator.StringToHash("Grounded");
        ANIM_JUMP_TRIGGER = Animator.StringToHash("Jump");
        ANIM_RUN_BOOL = Animator.StringToHash("Run");
        ANIM_FIXING_BOOL = Animator.StringToHash("fixing");

        isRight = playerNum == 1;
        sr = GetComponent<SpriteRenderer>();
        RB = transform.GetComponent<Rigidbody2D>();
        SetPlayerKeys(playerNum);
        anim = GetComponentInChildren<Animator>();
        graphicsRootTransform = anim.gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(ANIM_GROUNDED_BOOL, IsGrounded());
        HandlePlayerMovement();
        HandlePlayerActions();
    }

    private bool IsGrounded() {
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 1f);
        foreach (RaycastHit2D hit in hits){
            if (
                hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") ||
                hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform")) {
                return true;
            }
        }
        return false;


    }

    private void HandlePlayerActions() {
        if (Input.GetKeyDown(player_lift))
        {
            ActionButton();
        }
        if (pickup != null && pickup.CanRotate() && Input.GetKey(player_rotate))
        {
            pickup.transform.Rotate(new Vector3(0, 0, 2));
        }
        if (Input.GetKey(player_fix))
        {
            anim.SetBool(ANIM_FIXING_BOOL, true);
            if (CanFix())
            {
                this.potentialPart.Recharge();
            }
        }
        else if (potentialPart != null)
        {
            anim.SetBool(ANIM_FIXING_BOOL, false);
            potentialPart.SetNotCharging();
        }
        else {
            anim.SetBool(ANIM_FIXING_BOOL, false);
        }
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(player_left))
        {
            RB.velocity = new Vector2(-1f * __velocity, RB.velocity.y);
            isRight = false;
            anim.SetBool(ANIM_RUN_BOOL, true);
        }
        else if (Input.GetKey(player_right))
        {
            isRight = true;
            RB.velocity = new Vector2(1f*__velocity, RB.velocity.y);
            anim.SetBool(ANIM_RUN_BOOL, true);
        }
        else
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
            anim.SetBool(ANIM_RUN_BOOL, false);
        }
        Vector3 currGraphicsScale = graphicsRootTransform.transform.localScale;
        currGraphicsScale.x = isRight ? -Mathf.Abs(currGraphicsScale.x) : Mathf.Abs(currGraphicsScale.x);
        graphicsRootTransform.localScale = currGraphicsScale;

        if (Input.GetKeyDown(player_jump) && !is_jumping)
        {
            anim.SetTrigger(ANIM_JUMP_TRIGGER);
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
            case 2:
                player_left = KeyCode.LeftArrow;
                player_right = KeyCode.RightArrow;
                player_jump = KeyCode.UpArrow;
                player_lift = KeyCode.K;
                player_rotate = KeyCode.DownArrow;
                player_fix = KeyCode.L;
                break;
            case 1:
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
