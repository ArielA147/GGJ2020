using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakebite : MonoBehaviour
{

    Animator biteAnim;
    int ANIM_TRIGGER_BITE;

    // Start is called before the first frame update
    void Start()
    {

        ANIM_TRIGGER_BITE = Animator.StringToHash("bite");
        biteAnim = GetComponentInChildren<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        DebugInputHandling();
#endif

    }

    void DebugInputHandling() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Bite();
        }
        float axis = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(axis * Time.deltaTime, 0f));
    }

    void Bite() {
        biteAnim.SetTrigger(ANIM_TRIGGER_BITE);
    }


}
