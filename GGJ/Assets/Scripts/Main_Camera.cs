using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    public Camera cam;
    public Player [] players;
    float curr_size;
    public float players_max_dist;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectsOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 p1 = Camera.main.WorldToViewportPoint(players[0].transform.position);
        Vector2 p2 = Camera.main.WorldToViewportPoint(players[1].transform.position);
        if (Mathf.Abs(p1.y - p2.y) > 0.65f)
        {

            if ((p1.y > 0.8f || p2.y > 0.8f) && (p1.y < 0.01f || p2.y < 0.01f))
            {
                Camera.main.fieldOfView += 0.3f;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.37f, transform.position.z);

            }
            else if (p1.y > 0.8f || p2.y > 0.8f)
            {
                Camera.main.fieldOfView += 0.3f;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.37f, transform.position.z);

            }
            else if (p1.y < 0.01f || p2.y < 0.01f)
            {
                Camera.main.fieldOfView -= 0.3f;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.37f, transform.position.z);

            }
        }
        
        else if (Mathf.Abs(p1.y - p2.y) < 0.65f && Mathf.Abs(p1.y - p2.y) > 0.62f)
        {
            Debug.Log("heree");

            if (p1.y > 0.2f || p2.y > 0.2f)
            {
                Camera.main.fieldOfView -= 0.3f;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.37f, transform.position.z);

            }
            else if (p1.y < 0.65f || p2.y < 0.65f)
            {
                Camera.main.fieldOfView -= 0.3f;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.37f, transform.position.z);

            }

        }
        else if (p1.y < 0.13f || p2.y < 0.13f)
        {
            //Camera.main.fieldOfView -= 0.3f;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.37f, transform.position.z);

        }
        else if (Mathf.Abs(p1.y - p2.y)<0.65f && p1.y < 0.2f &&  p2.y < 0.2f)
        {
            //Camera.main.fieldOfView -= 0.3f;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.37f, transform.position.z);
        }

    }
}
   

