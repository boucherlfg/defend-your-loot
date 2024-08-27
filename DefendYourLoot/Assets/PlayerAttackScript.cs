using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float cost = 3;
    public float charge = 10;
    private float currentCharge;
    private GameObject instance;
    public GameObject projectile;
    private bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        currentCharge = charge;
        ServiceManager.Instance.Get<OnLevelStarted>().Subscribe(HandleLevelStarted);
        ServiceManager.Instance.Get<OnLevelDone>().Subscribe(HandleLevelDone);
    }

    private void HandleLevelStarted()
    {
        ServiceManager.Instance.Get<OnLevelStarted>().Unsubscribe(HandleLevelStarted);
        ServiceManager.Instance.Get<OnLevelDone>().Unsubscribe(HandleLevelDone);
        gameStarted = true;
    }

    private void HandleLevelDone(LevelDoneType type)
    {
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted) return;

        currentCharge += Time.deltaTime;
        currentCharge = Mathf.Min(charge, currentCharge);
        
        ServiceManager.Instance.Get<OnManaChanged>().Invoke(currentCharge / charge);
        if(currentCharge < cost) return;
        
        if(!Input.GetMouseButtonDown(0)) return;
        
        currentCharge -= cost;

        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = mouse - transform.position;
        delta = delta.normalized;

        if(instance) Destroy(instance);
        instance = Instantiate(projectile, transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().velocity = delta * 8;
    }
}
