using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Player 1
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;
    
    // Player 2
    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;
    
    // Ball
    public BallControl ball;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;
    
    // Max score
    public int maxScore;

    private void Start()
    {
        // Get components
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    private void OnGUI()
    {
        // Show player1's score in the left and
        GUI.Label(new Rect(Screen.width/2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        // player2's score in the right side
        GUI.Label(new Rect(Screen.width/2 + 150 + 12, 20, 100, 100), "" + player2.Score);
        
        // Restart button to play game from the beginning
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            // When restart button is pressed, reset score
            player1.ResetScore();
            player2.ResetScore();
            
            // ... and restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }
        
        // If player1 wins, ...
        if (player1.Score == maxScore)
        {
            // Show "Player One Won" on the left side
            GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 10, 2000, 1000), "Player One Won");
            
            // Reset ball to the center
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        } 
        // If player 2 wins, ...
        else if (player2.Score == maxScore)
        {
            // Show "Player Two Won" on the right side
            GUI.Label(new Rect(Screen.width/2 + 30, Screen.height/2 - 10, 2000, 1000), "Player Two Won");
            
            // Reset ball to the center
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }
}
