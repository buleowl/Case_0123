using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotion : MonoBehaviour
{
    Animator animator = null;
    bool walk = false;
    bool run = false;
    bool jump = false;
    bool fall = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        Motion();
    }

    void Motion()
    {

        bool wasd = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
        bool vh = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
        bool runPress = Input.GetKey(KeyCode.LeftShift);
        //bool jumpPress = Input.GetButtonDown("Jump");
        bool jumpPress = Input.GetKey(KeyCode.Space);
        if (wasd || vh)
        {
            walk = true;
        }
        else if (!wasd || !vh)
        {
            walk = false;
            run = false;
        }
        if (runPress)
        {
            run = true;
        }
        else 
        {
            run = false;
        }

        if (jumpPress)
        {
            jump = true;
        }
        else
        {
            jump = false;

        }
        animator.SetBool("isWalk", walk);
        animator.SetBool("isRun", run);
        animator.SetBool("isJump", jump);
        animator.SetBool("isFall", fall);
    }
}
