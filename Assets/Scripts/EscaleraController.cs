using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class EscaleraController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator myAnim;

    private PlayerController controller;

    public BoxCollider2D platformGround;

    [HideInInspector]
    public bool onLadder = false;

    public float climbSpeed;

    public float exitHop = 3f;
    
   
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Escalera") && onLadder)
        {
            rb.gravityScale = 10;
            onLadder = false;
            controller.usingLadder = onLadder;
            platformGround.enabled = true;
            
            myAnim.SetBool("onLadder",onLadder);
            if (!controller.isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, exitHop);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Escalera"))
        {
            if (Input.GetAxisRaw("Vertical")!=0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;
                platformGround.enabled = false;
                controller.usingLadder = onLadder;
            }
            else if(Input.GetAxisRaw("Vertical")==0 && onLadder)

            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            
            myAnim.SetBool("onLadder",onLadder);
            
        }
        
       
    }
}
