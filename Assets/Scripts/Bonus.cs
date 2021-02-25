using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private float fallBoundary = 120;
        

    // Start is called before the first frame update        
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    private void Update()
    {
        if (transform.position.y <= -fallBoundary)
        {
            Destroy(gameObject);
        }    
    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {

            Destroy(gameObject);
            
        }
    }

}
