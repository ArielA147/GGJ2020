using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotChunk : MonoBehaviour
{

    Robot parentRobot;

    // Start is called before the first frame update
    void Start()
    {
        parentRobot = GetComponentInParent<Robot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
