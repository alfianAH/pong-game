using UnityEngine;

namespace GamePlay
{
    public class Trajectory : MonoBehaviour
    {
        // Ball's components
        private BallControl ball;
        private CircleCollider2D ballCollider;
        private Rigidbody2D ballRigidbody;

        public Transform ballAtCollision; // Shadow ball
    
        private void Start()
        {
            ball = BallControl.Instance;
                
            ballCollider = ball.GetComponent<CircleCollider2D>();
            ballRigidbody = ball.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Initiate the path bounce state
            // which will only be displayed if the track collides
            // with a specific object
            bool drawBallAtCollision = false;
            Vector2 offsetHitPoint = new Vector2();
        
            // Set the point of collision with circle movement detection 
            RaycastHit2D[] circleCastHit2Ds = Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius,
                ballRigidbody.velocity.normalized);

            foreach (RaycastHit2D circleCastHit2D in circleCastHit2Ds)
            {
                // If there is a collision and the collision is not with the ball, ...
                // (Because the trajectory is drawn from the center of the ball)
                if (circleCastHit2D.collider == null ||
                    circleCastHit2D.collider.GetComponent<BallControl>() != null) continue;
                // A trajectory will be drawn from the center of the current ball to the center of the 
                // ball at collision,
                // Which is a point offset from the point of collision based on the normal vector
                // as big as the radius of the ball.
                
                // Set hitPoint
                Vector2 hitPoint = circleCastHit2D.point;
            
                // Set hitNormal in hitPoint
                Vector2 hitNormal = circleCastHit2D.normal;
            
                // Set offsetHitPoint, which is the center point of the ball at collision
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;
            
                // Draw dotted line from the center of the current ball to the center of ballAtCollision
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);
            
                // If not sideWall and not power up object, draw the reflection
                if (!circleCastHit2D.collider.CompareTag("NoReflection"))
                {
                    // Calculate inVector
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                
                    // Calculate outVector
                    Vector2 outVector = Vector2.zero;
                    if(circleCastHit2D.collider.CompareTag("Player"))
                    {
                        /*
                     * float angle = (transform.position.y - other.transform.position.y) * 5f;
                     * Vector2 direction = new Vector2(rigidbody2D.velocity.x, angle).normalized;
                     */
                        float angle = (ballAtCollision.position.y - circleCastHit2D.transform.position.y) * 5f;
                        outVector = new Vector2(-ballRigidbody.velocity.x, angle).normalized;
                    }
                    else
                    {
                        outVector = Vector2.Reflect(inVector, hitNormal);
                    }
                    // Calculate dot product from outVector and hitNormal.
                    // Is used so that the trajectory line when there is a collision, line will not be drawn
                    float outDot = Vector2.Dot(outVector, hitNormal);
                
                    if (outDot > -1.0f && outDot < 1.0f)
                    {
                        // Draw reflection trajectory
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector*10f);
                        // To draw shadow ball in hit point
                        drawBallAtCollision = true;
                    }
                }
                // Only draw trajectory for 1 hit point, so get out from the loop....
                break;
            }

            if (drawBallAtCollision)
            {
                // Draw shadow ball in hit point prediction
                ballAtCollision.position = offsetHitPoint;
                ballAtCollision.gameObject.SetActive(true);
            }
            else
            {
                // Hide shadow ball
                ballAtCollision.gameObject.SetActive(false);
            }
        }
    }
}
