using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHitable : MonoBehaviour
{

    Animator anim;
    private int ANIM_TRIGGER_BITE;

    // Start is called before the first frame update
    void Start()
    {
        ANIM_TRIGGER_BITE = Animator.StringToHash("hit");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit() {
        
        anim.SetTrigger(ANIM_TRIGGER_BITE);
    }

}
