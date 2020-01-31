using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    private Player [] players;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectsOfType<Player>();
        players = new Player[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
