using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("User Interface")]
        [SerializeField] private Slider ultiSlider;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject transparentBg;
        [SerializeField] private Text gamePausedText;
        [SerializeField] private Text resumeText;
        [SerializeField] private Text winnerText;
    
        [Header("Control")]
        [SerializeField] private BallControl ball;
        [SerializeField] private float hitPowerUp,
            scorePowerUp;

        // Buttons to move
        public KeyCode upButton = KeyCode.W,
            downButton = KeyCode.S;
    
        // KeyCode Power Up
        public KeyCode powerUpKey;

        public float speed = 10f, // Bat speed
            yBoundary = 9f; // y axis boundary

        private Rigidbody2D rb2D;
        private int score;
        // Last contact point with ball 
        private ContactPoint2D lastContactPoint;

        public ContactPoint2D LastContactPoint => lastContactPoint;

        public int Score => score; // Getter score

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Get bat's current speed
            Vector2 velocity = rb2D.velocity;

            // If player presses the upButton, ...
            if (Input.GetKey(upButton))
                velocity.y = speed; // then move upper
            // If player presses the downButton, ...
            else if (Input.GetKey(downButton))
                velocity.y = -speed; // then move down
            // Else, don't move
            else
                velocity.y = 0f;
        
            rb2D.velocity = velocity; // Update velocity

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
            if (Input.GetKeyDown(powerUpKey) && ultiSlider.value >= 1)
            {
                StartCoroutine(PowerUpDecreaseAnimation(0.05f));
                ball.xInitialForce += 100;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // If player collides with ball, record contact point
            if (!other.gameObject.name.Equals("Ball")) return;
            float targetValue = ultiSlider.value + hitPowerUp;
            StartCoroutine(PowerUpIncreaseAnimation(0.005f, targetValue));
            lastContactPoint = other.GetContact(0);
        }

        private IEnumerator PowerUpDecreaseAnimation(float waitTime)
        {
            for (float i = ultiSlider.value; i >= 0; i-=0.1f)
            {
                ultiSlider.value = i;
                yield return new WaitForSeconds(waitTime);
            }
        }

        private IEnumerator PowerUpIncreaseAnimation(float waitTime, float targetValue)
        {
            for (float i = ultiSlider.value; i <= targetValue; i += 0.01f)
            {
                ultiSlider.value = i;
                yield return new WaitForSeconds(waitTime);
            }
        }

        /// <summary>
        /// Increase score by 1
        /// </summary>
        public void IncrementScore()
        {
            score++;
            UpdateScoreText(score);
            CheckWinner();
            ball.IsPlayer1 = false;
            ball.IsPlayer2 = false;
            float targetValue = ultiSlider.value + scorePowerUp;
            StartCoroutine(PowerUpIncreaseAnimation(0.005f, targetValue));
        }

        /// <summary>
        /// Reset score to 0
        /// </summary>
        public void ResetScore()
        {
            score = 0;
            ultiSlider.value = 0; // Reset power
        }

        /// <summary>
        /// Update score text with player's score
        /// </summary>
        /// <param name="value">Player's score</param>
        private void UpdateScoreText(int value)
        {
            scoreText.text = value.ToString();
        }
    
        /// <summary>
        /// Check score if score is maxScore
        /// </summary>
        private void CheckWinner()
        {
            if (score == 5)
            {
                transparentBg.SetActive(true);
                resumeText.gameObject.SetActive(false);
                gamePausedText.gameObject.SetActive(false);
                winnerText.gameObject.SetActive(true);
                winnerText.text = $"{gameObject.name} WON!!";
            }
        }
    }
}
