using Core;
using Pathfinding;
using UnityEngine;

namespace AI
{
    public class AstarVelocity : MonoBehaviour, IVelocityProvider
    {
        private AIPath aiPath;
        public Vector2 Velocity => aiPath.velocity;
        void Start()
        {
            aiPath = GetComponent<AIPath>();
            if(!aiPath) aiPath = GetComponentInChildren<AIPath>();
            if(!aiPath) aiPath = GetComponentInParent<AIPath>();
        }

    }
}