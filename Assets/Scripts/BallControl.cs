using UnityEngine;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    // Initial Force to push the ball
    public float xInitialForce = 50,
        yInitialForce = 15;
    
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    /// <summary>
    /// Reset ball's position and velocity
    /// </summary>
    private void ResetBall()
    {
        // Reset position to (0,0)
        transform.position = Vector2.zero;
        
        // Reset velocity to (0,0)
        rigidbody2D.velocity = Vector2.zero;
    }

    /// <summary>
    /// Push Ball to random direction
    /// </summary>
    private void PushBall()
    {
        // Set random initial force
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        
        // Set randomDirection
        float randomDirection = Random.Range(0, 2);

        // If randomDirection is lower than 1, ...
        rigidbody2D.AddForce(randomDirection < 1f
            ? new Vector2(-xInitialForce, yRandomInitialForce) // Move to the left
            : new Vector2(xInitialForce, yRandomInitialForce)); // Else, move to the right
    }

    /// <summary>
    /// Restart game 
    /// </summary>
    private void RestartGame()
    {
        ResetBall(); // Reset ball
        Invoke(nameof(PushBall), 2f);
    }
}
