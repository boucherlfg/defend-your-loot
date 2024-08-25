using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private float biasTowardsPlayer = 5;
    [SerializeField]
    private float biasTowardsMouse = 1;
    private MoveScript player;
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnLevelLoaded>().Subscribe(HandleLevelLoaded);
    }
    void OnDestroy() {
        ServiceManager.Instance.Get<OnLevelLoaded>().Unsubscribe(HandleLevelLoaded);
    }
    private void HandleLevelLoaded(LevelScript script)
    {
        player = FindObjectOfType<MoveScript>();
        var pos = player.transform.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player) return;

        var pos = transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = Vector2.Lerp(pos, player.transform.position, Time.deltaTime * biasTowardsPlayer);
        pos = Vector2.Lerp(pos, mousePos, Time.deltaTime * biasTowardsMouse);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
