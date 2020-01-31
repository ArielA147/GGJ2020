using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_part : MonoBehaviour
{
    public enum State { DETTACHED, ATTACHED};
    private State curr_state;
    private Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        //RB = transform.GetComponent<Rigidbody2D>();
        //GameObject.Destroy(RB);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeState(State state)
    {
        curr_state = state;
        if (state == State.ATTACHED )
        {
            Debug.Log(" pickingup");
            if (RB != null)
            {
                GameObject.Destroy(RB);
            }
        }
        else if (RB == null && state != State.ATTACHED) { gameObject.AddComponent<Rigidbody2D>(); }
    }
    public State GetState()
    {
        return curr_state;
    }
}
