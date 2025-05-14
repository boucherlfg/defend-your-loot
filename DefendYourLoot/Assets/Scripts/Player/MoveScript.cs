using Events;
using UnityEngine;

namespace Player
{
    public class MoveScript : MonoBehaviour
    {
        public Rigidbody2D _rigidBody;
        public float speed;

        private OnLevelLoaded onLevelStarted;

        // Update is called once per frame
        void Update()
        {
            var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            _rigidBody.velocity = move * speed;
        }
    }
}
