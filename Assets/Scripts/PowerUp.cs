using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PlayerControl player1, 
        player2;
    [SerializeField] private BallControl ball;
    [SerializeField] private float maxLength = 5f,
        powerUpMaxTime = 5f;

    private CircleCollider2D powerUpCollider;
    private Vector3 player1OriginScale,
        player2OriginScale;
    private Transform player1Transform,
        player2Transform;
    private bool isPlayer1, isPlayer2;
    private float yBoundaryOrigin;
    
    private void Awake()
    {
        powerUpCollider = GetComponent<CircleCollider2D>();
        
        yBoundaryOrigin = player1.yBoundary;
        
        player1Transform = player1.GetComponent<Transform>();
        player2Transform = player2.GetComponent<Transform>();
        
        player1OriginScale = player1Transform.localScale;
        player2OriginScale = player2Transform.localScale;
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
        powerUpCollider.enabled = false; // Disable this object's collider
        
        isPlayer1 = false;
        isPlayer2 = false;
        
        // Limit power up time
        yield return new WaitForSeconds(powerUpMaxTime);
        
        // Back to normal
        for (float i = player.localScale.y; i >= player1OriginScale.y; i-=0.1f)
        {
            player.localScale = new Vector2(player.localScale.x, i);
            yield return new WaitForSeconds(waitTime);
        }
            
        playerControl.yBoundary = yBoundaryOrigin;
    }
}
