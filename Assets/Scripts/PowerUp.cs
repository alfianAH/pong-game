using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PlayerControl player1, 
        player2;
    [SerializeField] private BallControl ball;
    [SerializeField] private float maxLength = 5f,
        powerUpMaxTime = 5f,
        xBoundary = 3f;

    private Vector3 playerOriginScale;
    private Transform powerUpTransform;
    private Transform player1Transform,
        player2Transform;
    private bool isPlayer1, isPlayer2;
    private float yBoundaryOrigin;
    
    private void Awake()
    {
        powerUpTransform = GetComponent<Transform>();
        yBoundaryOrigin = player1.yBoundary;
        
        player1Transform = player1.GetComponent<Transform>();
        player2Transform = player2.GetComponent<Transform>();
        
        playerOriginScale = player1Transform.localScale;
    }

    private void Update()
    {
        if (isPlayer1)
        {
            StartCoroutine(GiantPowerUp(player1Transform, player1, 0.005f));
        } 
        else if (isPlayer2)
        {
            StartCoroutine(GiantPowerUp(player2Transform, player2, 0.005f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If other is ball, ...
        if (!other.GetComponent<BallControl>()) return;
        // Check if Player 1 hits the ball, ...
        if (ball.IsPlayer1 && !ball.IsPlayer2)
        {
            isPlayer1 = true;
        }
        // Check if Player 2 hits the ball, ...
        else if (!ball.IsPlayer1 && ball.IsPlayer2)
        {
            isPlayer2 = true;
        }
    }

    /// <summary>
    /// Make player long
    /// </summary>
    /// <param name="player"></param>
    /// <param name="playerControl"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    private IEnumerator GiantPowerUp(Transform player, PlayerControl playerControl, float waitTime)
    {
        // If player is null, yield break
        if (!player) yield break;
        // Power up the player
        for (float i = player.localScale.y; i <= maxLength; i+=0.1f)
        {
            player.localScale = new Vector2(player.localScale.x, i);
            yield return new WaitForSeconds(waitTime);
        }
        
        playerControl.yBoundary = 5f; // Change y boundary of player
        // Make random position of power up
        powerUpTransform.position = new Vector2(Random.Range(-xBoundary, xBoundary), 
            Random.Range(-yBoundaryOrigin, yBoundaryOrigin));

        isPlayer1 = false;
        isPlayer2 = false;
        
        // Limit power up time
        yield return new WaitForSeconds(powerUpMaxTime);
        
        // Back to normal
        for (float i = player.localScale.y; i >= playerOriginScale.y; i-=0.1f)
        {
            player.localScale = new Vector2(player.localScale.x, i);
            yield return new WaitForSeconds(waitTime);
        }
            
        playerControl.yBoundary = yBoundaryOrigin;
    }
}
