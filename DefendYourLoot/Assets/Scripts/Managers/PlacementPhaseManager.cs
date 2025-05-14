using System.Collections;
using System.Collections.Generic;
using Core;
using Events;
using Level;
using UnityEngine;
using Utility;

namespace Managers
{
    public class PlacementPhaseManager : MonoBehaviour
    {
        private Camera mainCam;
        private LevelScript currentLevel;
        private GameState gameState;
        [SerializeField] private SpriteRenderer placeholder;
        [SerializeField] private List<PlaceableItem> startingItems;

        private OnLevelStarted onLevelStarted;
        private OnShopClosed onShopClosed;
        private OnInventoryChanged onInventoryChanged;
        private IEnumerator Start()
        {
            onLevelStarted = ServiceManager.Instance.Get<OnLevelStarted>();
            onShopClosed = ServiceManager.Instance.Get<OnShopClosed>();
            onInventoryChanged = ServiceManager.Instance.Get<OnInventoryChanged>();
            
            mainCam = Camera.main;
            gameState = ServiceManager.Instance.Get<GameState>();
            currentLevel = FindObjectOfType<LevelScript>();
            onShopClosed.Subscribe(HandleShopClosed);
            onInventoryChanged.Subscribe(HandleInventoryChanged);
            
            // start on second frame
            yield return null;
            startingItems.ForEach(item => gameState.AddItem(item));
        }

        private void HandleInventoryChanged(List<PlaceableItem> inventory)
        {
            if (inventory.Count > 0) return;
            placeholder.gameObject.SetActive(false);
            onLevelStarted.Invoke();
        }

        private void HandleShopClosed()
        {
            gameState.State = gameState.CountItems <= 0 ? GameStateEnum.Level : GameStateEnum.Placement;
        }


        // Update is called once per frame
        private void Update()
        {
            if(gameState.State != GameStateEnum.Placement) return;
            
            if(gameState.CountItems >= 0) {
                HandlePlaceholder();
            }

            if(!Input.GetMouseButtonDown(0)) return;
        
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapCircle(mousePos, 0.2f);
            if(!hit || !hit.TryGetComponent<FloorScript>(out _)) return;

            var placeableItem = gameState.GetFirstItem();
            if(!placeableItem) return;
            gameState.RemoveItem(placeableItem);
            Instantiate(placeableItem, mousePos, Quaternion.identity, currentLevel.transform);
        }

        private void HandlePlaceholder() 
        {
            if(gameState.CountItems <= 0) {
                placeholder.gameObject.SetActive(false);
                return;
            }
            placeholder.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            placeholder.gameObject.SetActive(true);
            placeholder.sprite = gameState.GetFirstItem().Sprite;
        }
    }

}
