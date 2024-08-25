using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Vector2 lastVelocity;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer rend;
    private IVelocityProvider _velocityProvider;
    // Start is called before the first frame update
    void Start()
    {
        _velocityProvider = GetComponent<IVelocityProvider>();
    }



    // Update is called once per frame
    void Update()
    {
        
        Vector2 velocity = _velocityProvider.Velocity;

        if(velocity.magnitude > 0.1) lastVelocity = velocity;

        rend.flipX = lastVelocity.x > 0.1f;

        if(velocity.magnitude > 0.1f) anim.Play("Walk");
        else anim.Play("Idle");
    }
}
