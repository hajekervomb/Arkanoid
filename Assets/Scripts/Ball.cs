﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    //movement speed of the ball
    public float speed = 100f;
    private Rigidbody2D ballVelocity;
    private const int BallDamage = 1;

    public delegate void BallDeath();
    public static BallDeath ballDeath = delegate { };

    
    // Start is called before the first frame update
    void Start()
    {
        ballVelocity = GetComponent<Rigidbody2D>();
        //ballVelocity.velocity = Vector2.up * speed;
    }
               
    private void OnCollisionEnter2D(Collision2D col)
    {
        //this function is called whenever
        //the ball collides with something

        //Hit the racket?
        if(col.gameObject.tag == "Player")
        {
            //calculate hit factor
            float x = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);

            //Calcullate direction, set Length to 1
            Vector2 dir = new Vector2(x, 1).normalized; 
            
            //Set velocity with dir * speed
            ballVelocity.velocity = dir * speed;

            Debug.Log("Ball X position: " + transform.position.x);
            Debug.Log("Racket X position: " + col.transform.position.x);
            Debug.Log("Direction:" + dir);
            Debug.Log("Ball velocity: " + ballVelocity.velocity);            
        }

        if (col.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("HitPlayer");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("HitOther");
        }
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            IEnemy enemy = col.gameObject.GetComponent(typeof(IEnemy)) as IEnemy;
            enemy?.TakeDamage(BallDamage); 
        }
    }

    private float HitFactor (Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        //ascii art lol
        //
        //-1 -0.5  0  0.5  1  <-  x value
        //==================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    public void DestroyBall()
    {
        GameManager.Instance.KillPlayer();
        ballDeath();
        Destroy(gameObject);        
    }
}
