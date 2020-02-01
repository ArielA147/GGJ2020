using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotChunk : MonoBehaviour
{

    Robot parentRobot;
    Vector3 _startPosition;
    float shakeAmount = 0.075f;
    float shakeSpeed = 15f;
    float offset;
    public bool isBottomChumk = false;


    // Start is called before the first frame update
    void Start()
    {
        parentRobot = GetComponentInParent<Robot>();
        offset = Random.Range(0f, Mathf.PI);
        _startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBottomChumk)
        {
            transform.localPosition = _startPosition + new Vector3(0.0f, Mathf.Sin((offset + Time.time) * shakeSpeed) * shakeAmount, 0.0f);
        }
    }
}
