using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private int levelIndex = -1;
    public List<GameObject> levels;
    private OnLevelDone onLevelDone;
    
    public GameObject HUDMenu;
    public GameObject placeMenu;
    public GameObject winMenu;
    public GameObject looseMenu;
    public GameObject endMenu;
    private GameObject currentLevel;
    
    private void Start() {
        onLevelDone = ServiceManager.Instance.Get<OnLevelDone>();
        onLevelDone.Subscribe(HandleLevelDone);
        ServiceManager.Instance.Get<OnInventoryChanged>().Subscribe(HandleInventoryChanged);
    }

    private void HandleInventoryChanged(PlaceItemScript script)
    {
        if(script.inventory.Count <= 0) {
            placeMenu.SetActive(false);
            HUDMenu.SetActive(true);
            ServiceManager.Instance.Get<OnLevelStarted>().Invoke();
        }
    }

    private void OnDestroy() {
        onLevelDone.Unsubscribe(HandleLevelDone);
    }

    private void HandleLevelDone(LevelDoneType type)
    {
        HUDMenu.SetActive(false);
        if(type == LevelDoneType.Win) 
            winMenu.SetActive(true);
        else if(type == LevelDoneType.Lose) 
            looseMenu.SetActive(true);
    }

    public void GoToLevel(int level) {
        StartCoroutine(NextLevelInside());
        IEnumerator NextLevelInside() {
            ClearLevel();
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            levelIndex = level;
            LoadLevel();
        }
    }
    public void RestartLevel() {
        StartCoroutine(NextLevelInside());
        IEnumerator NextLevelInside() {
            ClearLevel();
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            LoadLevel();
        }
    }

    public void NextLevel() {
        StartCoroutine(NextLevelInside());
        IEnumerator NextLevelInside() {
            ClearLevel();
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            levelIndex++;
            LoadLevel();
        }
    }
    private void ClearLevel() {
        if(currentLevel) Destroy(currentLevel);
    }
    private void LoadLevel() {
        if(levelIndex >= 0 && levelIndex < levels.Count) {
            currentLevel = Instantiate(levels[levelIndex], Vector2.zero, Quaternion.identity, transform);
            placeMenu.SetActive(true);
        }
        else endMenu.SetActive(true);
    }
}
