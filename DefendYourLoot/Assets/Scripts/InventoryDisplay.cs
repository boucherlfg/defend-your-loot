using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Transform container;
    public GameObject inventoryItem;
    private OnInventoryChanged onInventoryChanged;
    // Start is called before the first frame update
    void Start()
    {
        onInventoryChanged = ServiceManager.Instance.Get<OnInventoryChanged>();
        onInventoryChanged.Subscribe(HandleInventoryChanged);
    }
    void OnDestroy() {
        onInventoryChanged.Unsubscribe(HandleInventoryChanged);
    }

    private void HandleInventoryChanged(PlaceItemScript script)
    {
        foreach(Transform t in container) Destroy(t.gameObject);
        foreach(var item in script.inventory) {
            var instance = Instantiate(inventoryItem, container);
            instance.SetActive(true);
            instance.GetComponentInChildren<Image>().sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
