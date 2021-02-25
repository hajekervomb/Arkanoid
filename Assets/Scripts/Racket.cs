using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private float speed = 150f;
    
    private float racketWidth = 33f;

    
    
    private float horizontalMove;

    private Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D bc;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // get horizontal input
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        
    }
            
    private void Move()
    {
        rb.velocity = Vector2.right * horizontalMove * speed * Time.fixedDeltaTime * 100;
        //set velocity = movement direction * speed
        
    }
}
