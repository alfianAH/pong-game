using UnityEngine;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    // Initial Force to push the ball
    public float xInitialForce = 50,
        yInitialForce = 15;
    
    private Rigidbody2D rigidbody2D;
    private Vector2 trajectoryOrigin;

    public Vector2 TrajectoryOrigin => trajectoryOrigin;

    private void Start()
    {
        trajectoryOrigin = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    /// <summary>
    /// When ball collide with others, record contact point
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionExit2D(Collision2D other)
    {
        trajectoryOrigin = transform.position;
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
        // float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        
        // Set randomDirection
        float randomDirection = Random.Range(0, 2);
        
        // If randomDirection is lower than 1, ...
        rigidbody2D.AddForce(randomDirection < 1f
            ? new Vector2(-xInitialForce, yInitialForce) // Move to the left
            : new Vector2(xInitialForce, yInitialForce)); // Else, move to the right
    }

    /// <summary>
    /// Restart game 
    /// </summary>
    private void RestartGame()
    {
        ResetBall(); // Reset ball
        Invoke("PushBall", 2f);
    }
}
