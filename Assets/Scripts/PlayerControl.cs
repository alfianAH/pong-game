using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Slider ultiPowerUp;
    [SerializeField] private float hitPowerUp,
        scorePowerUp;
    [SerializeField] private BallControl ball;
    
    // Buttons to move
    public KeyCode upButton = KeyCode.W,
        downButton = KeyCode.S;
    
    // KeyCode Power Up
    public KeyCode powerUpKey;

    public float speed = 10f, // Bat speed
        yBoundary = 9f; // y axis boundary

    private Rigidbody2D rigidbody2D;
    private int score;
    // Last contact point with ball 
    private ContactPoint2D lastContactPoint;

    public ContactPoint2D LastContactPoint => lastContactPoint;

    private bool canUsePowerUp;
    
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
        
        // Power Up
        if (Input.GetKeyDown(powerUpKey) && ultiPowerUp.value >= 1)
        {
            StartCoroutine(PowerUpDecreaseAnimation(0.05f));
            IncrementScore();
            ball.RestartGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If player collides with ball, record contact point
        if (!other.gameObject.name.Equals("Ball")) return;
        ultiPowerUp.value += hitPowerUp;
        lastContactPoint = other.GetContact(0);
    }

    private IEnumerator PowerUpDecreaseAnimation(float waitTime)
    {
        for (float i = ultiPowerUp.value; i >= 0; i-=0.1f)
        {
            ultiPowerUp.value = i;
            yield return new WaitForSeconds(waitTime);
        }
    }

    /// <summary>
    /// Increase score by 1
    /// </summary>
    public void IncrementScore()
    {
        score++;
        ultiPowerUp.value += scorePowerUp;
    }

    /// <summary>
    /// Reset score to 0
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        ultiPowerUp.value = 0; // Reset power
    }
}
