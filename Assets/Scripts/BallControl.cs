using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    // Initial Force to push the ball
    public float xInitialForce = 50,
        yInitialForce = 15;

    [SerializeField] private bool isPlayer1,
        isPlayer2;

    [SerializeField] private Text homeBtn;
    
    private Rigidbody2D rigidbody2D;
    private Vector2 trajectoryOrigin;

    public bool IsPlayer1 => isPlayer1;
    public bool IsPlayer2 => isPlayer2;

    public Vector2 TrajectoryOrigin => trajectoryOrigin;

    private void Start()
    {
        trajectoryOrigin = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If other game object is player, ...
        if (!other.gameObject.CompareTag("Player")) return;
        // Check player's name
        switch (other.gameObject.name)
        {
            // If Player1, ...
            case "Player1":
                isPlayer1 = true; // Set isPlayer1 to true 
                isPlayer2 = false; // Set isPlayer2 to false
                break;
            // If Player2, ...
            case "Player2":
                isPlayer1 = false; // Set isPlayer1 to false
                isPlayer2 = true; // Set isPlayer2 to true
                break;
        }
        
        // Change ball's angle when hit bat (player)
        /* Illustration
         * bat   ball
         * |-|  /y = 5
         * | | /
         * | |/
         * | |---y = 0
         * | |\
         * | | \
         * |-|  \y = -4
         * bat   ball
         */
        float angle = (transform.position.y - other.transform.position.y) * 5f;
        Vector2 direction = new Vector2(rigidbody2D.velocity.x, angle).normalized;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(direction*xInitialForce);
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
    public void RestartGame()
    {
        if (homeBtn.gameObject.activeInHierarchy)
        {
            homeBtn.gameObject.SetActive(false);
        }
        ResetBall(); // Reset ball
        Invoke("PushBall", 2f);
    }
}
