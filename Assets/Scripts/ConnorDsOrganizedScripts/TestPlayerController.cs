using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator anim;
    private Rigidbody2D rb;

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

        if (Input.GetButtonDown("Horizontal"))
        {
            rb.velocity = Vector3.right * horiz * speed;

            if (horiz > 0)
            {
                anim.SetTrigger("WalkRight");
            } 
            else if (horiz < 0)
            {
                anim.SetTrigger("WalkLeft");
            }
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            rb.velocity = Vector3.up * vert * speed;

            if (vert > 0)
            {
                anim.SetTrigger("WalkBackwards");
            } 
            else if (vert < 0)
            {
                anim.SetTrigger("WalkForward");
            }
        } 
        else if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            anim.SetTrigger("Idle");
            rb.velocity = Vector3.zero;
        }
    }
}
