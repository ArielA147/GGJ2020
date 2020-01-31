using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float __velocity;
    private Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    private void Controller()
    {
        if (Input.GetButton("Horizontal"))
        {
            RB.velocity = new Vector2(Input.GetAxis("Horizontal")*__velocity, RB.velocity.y);
        }
    }
}
