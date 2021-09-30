using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocityX = 10;
    public float jumpforce = 50;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator _animator;
    
    

    public GameObject rightBall;
    public GameObject leftBall;
    private ScoreController game;
    
    private bool isDead = false;

    
    [HideInInspector]
    public bool isGrounded = true;
    
    [HideInInspector]
    public bool usingLadder = false;
    
    
    
    
    //VARIABLES CAIDA
    public bool isFalling;
    public Transform Pie;
    public float Radio = 0.6f;
    public LayerMask Suelo;
    public bool IsInGround;
    public float TimeTodie = 0f;

    public float vida = 100f;

    public float direccion = 0;

    public float DañoAlCaer = 100f;
    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        game = FindObjectOfType<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            IsInGround = Physics2D.OverlapCircle(Pie.position,Radio,Suelo);
            rb.velocity = new Vector2(0, rb.velocity.y);
            _animator.SetInteger("Estado", 0);

            if (IsInGround == false && rb.velocity.y < 0)
            {
                isFalling = true;
                TimeTodie += Time.deltaTime;
            }else if(IsInGround)
            {
                isFalling = false;
                if (TimeTodie >= 1.2f)
                {
                    vida -= DañoAlCaer;
                    TimeTodie = 0f;
                    if (vida<=0)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                else
                {
                    TimeTodie = 0f;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(velocityX, rb.velocity.y);
                sr.flipX = false;
                _animator.SetInteger("Estado", 1);
            }
            if (Input.GetKey((KeyCode.RightArrow))&&(Input.GetKey(KeyCode.C)))
            {
                isFalling = false;
                TimeTodie = 0f;
                rb.velocity = new Vector2(velocityX, rb.velocity.y);
                sr.flipX = false;
                _animator.SetInteger("Estado", 4);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-velocityX, rb.velocity.y);
                sr.flipX = true;
                _animator.SetInteger("Estado", 1);
            }
            
            if (Input.GetKey((KeyCode.LeftArrow))&&(Input.GetKey(KeyCode.C)))
            {
                isFalling = false;
                TimeTodie = 0f;
                rb.velocity = new Vector2(-velocityX, rb.velocity.y);
                sr.flipX = true;
                _animator.SetInteger("Estado", 4);
                
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
                _animator.SetInteger("Estado", 2);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = Vector2.right*velocityX;
                sr.flipX = false;
                _animator.SetInteger("Estado", 3);

            }
            
            if (Input.GetKeyDown(KeyCode.X)  )
            {
                Debug.Log("BALAAAAAAAAAAAA CHICA");
                var bullet = sr.flipX ? leftBall : rightBall;
                var position = new Vector2(transform.position.x,transform.position.y);
                var rotation = rightBall.transform.rotation;
                Instantiate(bullet, position, rotation);
                _animator.SetInteger("Estado", 5);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            game.LoseLife();
        }
    }
}
