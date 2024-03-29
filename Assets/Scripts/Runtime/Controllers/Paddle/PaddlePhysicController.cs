using System;
using Runtime.Interfaces;
using UnityEngine;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Paddle
{
    public class PaddlePhysicController : MonoBehaviour
    {
        [SerializeField] private float maxBounceAngle = 50f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Debug.Log("Ontriggerball");
               
            }
        }
    }
}