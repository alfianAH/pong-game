using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Trajectory for drawing ball trajectory prediction
    public Trajectory trajectory;
    
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

    private bool isDebugWindowShown = false;

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

        // If isDebugWindowShown, show text area for debug window
        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            
            // Save physic variables
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                $"Ball mass = {ballMass}\n" +
                $"Ball velocity = {ballVelocity}\n" +
                $"Ball speed = {ballSpeed}\n" +
                $"Ball momentum = {ballMomentum}\n" +
                $"Ball friction = {ballFriction}\n" +
                $"Last impulse from player 1 = ({impulsePlayer1X}, {impulsePlayer1Y})\n" +
                $"Last impulse from player 2 = ({impulsePlayer2X}, {impulsePlayer2Y})\n";
            
            // Show debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
            
            // Set GUI's oldColor
            GUI.backgroundColor = oldColor;
        }
        
        // Update isDebugWindowShown when player click the button
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}
