using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton

    public static BallsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private Transform racket;

    [SerializeField] private Ball ballPrefab;
    private Ball initialBall;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 150f;
    // if ball reach this Y coordinate -> destroy ball
    //[SerializeField] private float fallBoundary = -115f;

    public List<Ball> Balls { get; set; }

    private GameManager gm;
    
    private void Start()
    {
        gm = GameManager.Instance;
        Balls = new List<Ball>();
        InitBall();        
        
    }

    private void Update()
    {
        // check for: game was started?
        // if no, make ball leap to racket
        if (GameManager.Instance.isGameStarted == false)
        {
            Vector3 ballPosition = new Vector3(racket.position.x, racket.position.y + 8f, 0);
            if (initialBall != null)
            {
                initialBall.transform.position = ballPosition;
            }
            
        }
        

        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.isGameStarted == false)
        {   if (initialBall !=  null)
            {
                GameManager.Instance.isGameStarted = true;
                rb.velocity = Vector2.up * speed;
            }
                                   
        }               
                       
    }

    public void InitBall()
    {
        Vector3 startingPosition = new Vector3(racket.position.x, racket.position.y + 8f, 0); //get it from paddle
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);

        rb = initialBall.GetComponent<Rigidbody2D>();

        Balls.Add(initialBall);
        Debug.Log("Balls count: " + Balls.Count);

    }

    // make method that destroy our ball if it reach bottom of the screen
    // depricated
    /*
    public void DestroyBall()
    {        
        for (int i = Balls.Count - 1; i >= 0; i--)
        {
            // check if != null
            if (Balls[i] != null)
            {
                if (Balls[i].gameObject.transform.position.y < fallBoundary)
                {                    
                    Destroy(Balls[i].gameObject); //i guess this ball was removed from list Balls
                    Balls.Remove(Balls[i]);
                    Debug.Log("Balls count: " + Balls.Count);

                }

                if (Balls.Count == 0)
                {
                    GameManager.Instance.KillPlayer();
                    Debug.Log("KILL");
                    break;
                }
            }
            
        }
    }
    */
    
}
