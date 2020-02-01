using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{

    public int robotNumber;
    Animator anim;
    private int ANIM_ROBOT1_BOOL;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ANIM_ROBOT1_BOOL = Animator.StringToHash("isRobot1");
        anim.SetBool(ANIM_ROBOT1_BOOL, robotNumber == 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
