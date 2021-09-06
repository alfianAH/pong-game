using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GamePlay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Text debugInfoText;
        [SerializeField] private GameObject debugInfo;
        // [SerializeField] private GameObject transparentBg;
    
        // Trajectory for drawing ball trajectory prediction
        public Trajectory trajectory;
    
        // Player 1
        public PlayerControl player1;

        // Player 2
        public PlayerControl player2;

        // Ball
        private BallControl ball;
        private Rigidbody2D ballRigidbody;
        private CircleCollider2D ballCollider;
    
        // Max score
        public int maxScore;
    
        private bool isDebugWindowShown;

        public bool IsDebugWindowShown
        {
            get => isDebugWindowShown;
            set => isDebugWindowShown = value;
        }

        private void Start()
        {
            ball = BallControl.Instance;
            
            // Get components
            ballRigidbody = ball.GetComponent<Rigidbody2D>();
            ballCollider = ball.GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            // If isDebugWindowShown, show text area for debug window
            if (isDebugWindowShown)
            {
                // Save physic variables
                float ballMass = ballRigidbody.mass;
                Vector2 ballVelocity = ballRigidbody.velocity;
                float ballSpeed = ballVelocity.magnitude;
                Vector2 ballMomentum = ballMass * ballVelocity;
                float ballFriction = ballCollider.friction;

                float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
                float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            
                float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
                float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

                string debugText =
                    $"Ball mass = {ballMass:0.##}\n" +
                    $"Ball velocity = {ballVelocity}\n" +
                    $"Ball speed = {ballSpeed:0.##}\n" +
                    $"Ball momentum = {ballMomentum}\n" +
                    $"Ball friction = {ballFriction:0.##}\n" +
                    $"Last impulse from player 1 = ({impulsePlayer1X:0.##}, {impulsePlayer1Y:0.##})\n" +
                    $"Last impulse from player 2 = ({impulsePlayer2X:0.##}, {impulsePlayer2Y:0.##})\n";

                debugInfoText.text = debugText;
            }
        }

        /// <summary>
        /// Restart game when restart button is pressed
        /// </summary>
        public void RestartGame()
        {
            AudioManager.Instance.Play(ListSound.ButtonClick);
            SceneManager.LoadScene("Pong");
        }
    
        /// <summary>
        /// To show debug info
        /// </summary>
        public void ToggleDebugInfo()
        {
            AudioManager.Instance.Play(ListSound.ButtonClick);
            isDebugWindowShown = !isDebugWindowShown;
            debugInfo.SetActive(isDebugWindowShown);
            trajectory.enabled = !trajectory.enabled;
        }
    
        // Uncomment this if want to see how to make GUI programmatically
        /*private void OnGUI()
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
            transparentBg.gameObject.SetActive(true);
            // Reset ball to the center
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        } 
        // If player 2 wins, ...
        else if (player2.Score == maxScore)
        {
            // Show "Player Two Won" on the right side
            GUI.Label(new Rect(Screen.width/2 + 30, Screen.height/2 - 10, 2000, 1000), "Player Two Won");
            transparentBg.gameObject.SetActive(true);
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
    }*/
    }
}
