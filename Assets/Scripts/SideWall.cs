using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public PlayerControl player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If ball collide with this wall, ...
        if (other.name == "Ball")
        {
            player.IncrementScore(); // Add score to player
            
            // If player's score is less than max score, ...
            if (player.Score < gameManager.maxScore)
            {
                // Restart game
                other.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
