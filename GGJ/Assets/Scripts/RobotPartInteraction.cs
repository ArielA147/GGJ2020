using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartInteraction : MonoBehaviour
{

    RobotBasePart robotPart;

    // Start is called before the first frame update
    void Start()
    {
        robotPart = GetComponentInParent<RobotBasePart>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // to find the most updated place for the RBP
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("RobotChunk"))
        {
            this.robotPart.potentialRobotChunk = collision.GetComponent<RobotChunk>();
        }

    }

    // to check that we are out from the old RPB area and not in a new one
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("RobotChunk") &&
            this.robotPart.potentialRobotChunk != null &&
            GameObject.ReferenceEquals(
                collision.GetComponent<RobotChunk>().gameObject, 
                this.robotPart.potentialRobotChunk.gameObject))
        {
            this.robotPart.potentialRobotChunk = null;
        }
    }
}
