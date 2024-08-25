using Pathfinding;
using UnityEngine;

public class RigidbodyVelocity : MonoBehaviour, IVelocityProvider
{
    private Rigidbody2D _rigidBody;
    public Vector2 Velocity => _rigidBody.velocity;
    void Start(){
        _rigidBody = GetComponent<Rigidbody2D>();
    }
}