using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Ball")
        {
            Ball ball = collision.GetComponent<Ball>();
            BallsManager.Instance.Balls.Remove(ball);
            Debug.Log("BALLS COUNT: " + BallsManager.Instance.Balls.Count);
            ball.DestroyBall();
        }
    }
}
