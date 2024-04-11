using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.Paddle
{
    public class PaddleEyeFollowController : MonoBehaviour
    {
        private void Update()
        {
            FollowTarget();
        }
    
        private void FollowTarget(){
            
            var ball = FindObjectOfType<BallManager>().transform.position;

            var eyeTransform = transform;
            var position = eyeTransform.position;
            var direction = new Vector2(
                (ball.x - position.x),
                (ball.y - position.y)     
            );
            eyeTransform.up = direction;
        }
    }
}