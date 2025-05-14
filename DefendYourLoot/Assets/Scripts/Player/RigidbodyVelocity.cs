using Core;
using UnityEngine;

namespace Player
{
    public class RigidbodyVelocity : MonoBehaviour, IVelocityProvider
    {
        private Rigidbody2D _rigidBody;
        public Vector2 Velocity => _rigidBody.velocity;

        private void Start(){
            _rigidBody = GetComponent<Rigidbody2D>();
        }
    }
}