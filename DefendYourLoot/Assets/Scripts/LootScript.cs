using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public static int money = 0;
    private bool levelDone = false;
    private static List<LootScript> loot = new();
    // Start is called before the first frame update
    void Start()
    {
        loot.Add(this);
        ServiceManager.Instance.Get<OnLevelDone>().Subscribe(HandleLevelDone);
    }

    private void HandleLevelDone(LevelDoneType type)
    {
        if(levelDone) return;
        if(type == LevelDoneType.Win) {
            levelDone = true;
            money++;
            ServiceManager.Instance.Get<OnMoneyChanged>().Invoke(money);
        }
    }

    void OnDestroy() {
        loot.Remove(this);

        if(levelDone) return;
        if(loot.Count > 0) return;
        
        ServiceManager.Instance.Get<OnLevelDone>().Invoke(LevelDoneType.Lose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
