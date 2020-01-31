using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotPartBase : MonoBehaviour
{
    private int lives;
    public int Lives {
        get{return lives;}
        set { lives = value; }
    }

    void RaisingLives(){
        this.lives ++ ;
    }

    void DecreasingLives(){
        if(this.lives> 0){
            this.lives --; 
        }
    }

    void PrintRemainingLives(){
        Console.WriteLine(lives);
    }

    // Start is called before the first frame update
    void Start()
    {
        RobotPartBase r = new RobotPartBase();
        r.Lives = 3;
        PrintRemainingLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
