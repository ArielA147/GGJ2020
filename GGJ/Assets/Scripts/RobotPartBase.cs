using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotPartBase : MonoBehaviour
{
    private int lives;
    public int Lives {
        get{return lives;}
    }

    void PrintRemainingLives(){
        Console.WriteLine(lives);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
