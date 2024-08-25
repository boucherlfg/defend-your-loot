using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    private List<GameObject> heroes = new();
    public float spawnInterval = 3;
    private float timer;
    public GameObject prefab;
    public int heroCount = 3;
    private bool running;
    void Start() {
        ServiceManager.Instance.Get<OnLevelStarted>().Subscribe(HandleLevelStarted);
    }

    private void HandleLevelStarted()
    {
        ServiceManager.Instance.Get<OnLevelStarted>().Unsubscribe(HandleLevelStarted);
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!running) return;
        heroes.RemoveAll(x => !x);
        if(heroCount <= 0 && heroes.Count <= 0) {
            ServiceManager.Instance.Get<OnLevelDone>().Invoke(LevelDoneType.Win);
        }

        if(heroCount <= 0) return;
        timer += Time.deltaTime;
        if(timer < spawnInterval) return;
        timer = 0;

        heroes.Add(Instantiate(prefab, transform.position, Quaternion.identity, transform));
        heroCount--;
    }
}
