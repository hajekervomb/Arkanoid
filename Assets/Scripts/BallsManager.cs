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

    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
        
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted == false)
        {
            Vector3 ballPosition = new Vector3(racket.position.x, racket.position.y + 8f, 0);
            initialBall.transform.position = ballPosition;
        }
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.isGameStarted = true;
            rb.velocity = Vector2.up * speed;
        }
    }

    private void InitBall()
    {
        Vector3 startingPosition = new Vector3(racket.position.x, racket.position.y + 8f, 0); //get it from paddle
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);

        rb = initialBall.GetComponent<Rigidbody2D>();

        Balls = new List<Ball>
        {
            initialBall
        };
    }
}
