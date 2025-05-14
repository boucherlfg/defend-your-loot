using System.Collections.Generic;
using Core;
using Events;
using Level;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class InventoryDisplay : MonoBehaviour
    {
        public Transform container;
        public InventoryItem inventoryItem;
        private OnInventoryChanged onInventoryChanged;
        private GameState gameState;
        // Start is called before the first frame update
        private void Start()
        {
            onInventoryChanged = ServiceManager.Instance.Get<OnInventoryChanged>();
            gameState = ServiceManager.Instance.Get<GameState>();
            
            onInventoryChanged.Subscribe(HandleInventoryChanged);
        }

        private void OnDestroy() {
            onInventoryChanged.Unsubscribe(HandleInventoryChanged);
        }

        private void HandleInventoryChanged(List<PlaceableItem> items)
        {
            foreach(Transform t in container) Destroy(t.gameObject);
            foreach(var item in items)
            {
                var instance = Instantiate(inventoryItem, container);
                instance.gameObject.SetActive(true);
                instance.Sprite = item.Sprite;
            }
        }
    }
}
