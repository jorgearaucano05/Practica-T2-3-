using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float velocityX = 10f;

    private Rigidbody2D rb;
    private ScoreController _scoreController;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _scoreController = FindObjectOfType<ScoreController>();
        Destroy(this.gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.CompareTag("Enemigo"))
        {
           
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _scoreController.PlusScore(10);
            Debug.Log(_scoreController.GetScore());
                
                
        }
            
    }
}
