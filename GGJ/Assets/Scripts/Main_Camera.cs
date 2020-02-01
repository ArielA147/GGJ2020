using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    public Camera cam;
    public Player [] players;
    float curr_size;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectsOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {/*
        foreach (Player player in players)
        {
            if ( cam.WorldToScreenPoint( player.transform.position ).y > Screen.height && 
                Mathf.Abs((players[0].transform.position.y - players[1].transform.position.y) ) < Screen.height)
            {
                Debug.Log("ttt");
                curr_size = cam.orthographicSize;
                cam.orthographicSize += 0.1f;
            }
            else if (player.transform.GetComponent<Rigidbody2D>().velocity.y < 0 && cam.WorldToScreenPoint(player.transform.position).y < Screen.height/2f )
            {
                //cam.orthographicSize -= 0.1f;
            }
        } */
    }
   
}
