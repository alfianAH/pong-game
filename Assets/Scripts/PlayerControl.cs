using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Buttons to move
    public KeyCode upButton = KeyCode.W,
        downButton = KeyCode.S;

    public float speed = 10f, // Bat speed
        yBoundary = 9f; // y axis boundary

    private Rigidbody2D rigidbody2D;
    private int score;
    
    public int Score => score; // Getter score

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get bat's current speed
        Vector2 velocity = rigidbody2D.velocity;

        // If player presses the upButton, ...
        if (Input.GetKey(upButton))
            velocity.y = speed; // then move upper
        // If player presses the downButton, ...
        else if (Input.GetKey(downButton))
            velocity.y = -speed; // then move down
        // Else, don't move
        else
            velocity.y = 0f;
        
        rigidbody2D.velocity = velocity; // Update velocity

        // Get bat's current position
        Vector3 position = transform.position;

        // If bat's position get past the upper boundary, ...
        if (position.y > yBoundary)
            position.y = yBoundary; // Set position to yBoundary
        // If bat's position get past the lower boundary, ...
        else if (position.y < -yBoundary)
            position.y = -yBoundary; // Set position to -yBoundary

        transform.position = position; // Update position
    }

    /// <summary>
    /// Increase score by 1
    /// </summary>
    public void IncrementScore()
    {
        score++;
    }

    /// <summary>
    /// Reset score to 0
    /// </summary>
    public void ResetScore()
    {
        score = 0;
    }
}
