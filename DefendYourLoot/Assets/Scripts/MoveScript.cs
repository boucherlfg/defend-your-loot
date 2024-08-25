using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Rigidbody2D _rigidBody;
    public float speed;

    private OnLevelLoaded onLevelStarted;
    // Start is called before the first frame update
    void Start()
    {
        onLevelStarted = ServiceManager.Instance.Get<OnLevelLoaded>();
        onLevelStarted.Subscribe(HandleLevelStarted);
    }

    void OnDestroy() {
        onLevelStarted.Unsubscribe(HandleLevelStarted);
    }

    private void HandleLevelStarted(LevelScript script)
    {
        transform.position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rigidBody.velocity = move * speed;
    }
}
