using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    private static List<HeroSpawner> spawners = new();
    private static List<GameObject> heroes = new();
    public float spawnInterval = 3;
    private float timer;
    public GameObject prefab;
    public int heroCount = 3;
    private bool running;
    void Start() {
        ServiceManager.Instance.Get<OnLevelStarted>().Subscribe(HandleLevelStarted);
        spawners.Add(this);
    }
    void OnDestroy() {
        spawners.Remove(this);
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
        if(spawners.All(x => x.heroCount <= 0) && heroes.Count <= 0) {
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
