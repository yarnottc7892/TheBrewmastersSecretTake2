using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator anim;
    private Rigidbody2D rb;

    private string idle = "IdleForward";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (horiz != 0)
        {
            rb.velocity = Vector3.right * horiz * speed;

            if (horiz > 0 && idle != "IdleRight")
            {
                anim.SetTrigger("WalkRight");
                idle = "IdleRight";
            } 
            else if (horiz < 0 && idle != "IdleLeft")
            {
                anim.SetTrigger("WalkLeft");
                idle = "IdleLeft";
            }
        }
        else if (vert != 0)
        {
            rb.velocity = Vector3.up * vert * speed;

            if (vert > 0 && idle != "IdleBackwards")
            {
                anim.SetTrigger("WalkBackwards");
                idle = "IdleBackwards";
            } 
            else if (vert < 0 && idle != "IdleForward")
            {
                anim.SetTrigger("WalkForward");
                idle = "IdleForward";
            }
        } 
        else
        {
            anim.SetTrigger(idle);
            rb.velocity = Vector3.zero;
        }
    }
}
