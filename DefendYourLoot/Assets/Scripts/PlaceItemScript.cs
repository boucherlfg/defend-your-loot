using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceItemScript : MonoBehaviour
{
    private LevelScript currentLevel;
    private OnLevelLoaded onLevelLoaded;
    private OnInventoryChanged onInventoryChanged;
    public List<GameObject> inventory;
    public GameObject placeholer;
    private bool playing;
    // Start is called before the first frame update
    void Start()
    {
        onLevelLoaded = ServiceManager.Instance.Get<OnLevelLoaded>();
        onInventoryChanged = ServiceManager.Instance.Get<OnInventoryChanged>();
        ServiceManager.Instance.Get<OnLevelStarted>().Subscribe(HandleLevelStarted);
        onLevelLoaded.Subscribe(HandleLevelLoaded);
    }

    private void HandleLevelStarted()
    {
        playing = true;
        placeholer.SetActive(false);
    }

    private void HandleLevelLoaded(LevelScript script)
    {
        playing = false;
        currentLevel = script;
        foreach(var item in currentLevel.availableItems) {
            inventory.Insert(0, item);
        }
        onInventoryChanged.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(playing) return;
        if(inventory.Count >= 0) {
            HandlePlaceholder();
        }

        if(!Input.GetMouseButtonDown(0)) return;


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.OverlapCircle(mousePos, 0.2f);
        if(!hit || !hit.GetComponent<FloorScript>()) return;

        var instance = inventory.FirstOrDefault();
        if(!instance) return;
        inventory.RemoveAt(0);
        instance = Instantiate(instance, mousePos, Quaternion.identity, currentLevel.transform);
        onInventoryChanged.Invoke(this);
    }
    void HandlePlaceholder() {
        if(inventory.Count <= 0) {
            placeholer.SetActive(false);
            return;
        }
        placeholer.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placeholer.SetActive(true);
        placeholer.GetComponent<SpriteRenderer>().sprite = inventory.First().GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
