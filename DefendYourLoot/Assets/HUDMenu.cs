using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMenu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnLevelStarted>().Subscribe(HandleLevelStarted);
        ServiceManager.Instance.Get<OnLevelDone>().Subscribe(HandleLevelDone);
    }

    void OnDestroy() {
        ServiceManager.Instance.Get<OnLevelStarted>().Unsubscribe(HandleLevelStarted);
        ServiceManager.Instance.Get<OnLevelDone>().Unsubscribe(HandleLevelDone);
    }
    private void HandleLevelDone(LevelDoneType type)
    {
        menu.SetActive(false);
    }

    private void HandleLevelStarted()
    {
        menu.SetActive(true);
    }
}
