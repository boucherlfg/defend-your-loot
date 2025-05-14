using System.Collections.Generic;
using AI;
using Config;
using Core;
using Events;
using Level;
using Player;
using UnityEngine;
using Utility;

namespace Managers
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour 
    {
        private int levelIndex = -1;
        private OnLevelDone onLevelDone;
        private OnShopClosed onShopClosed;
        private OnLevelStarted onLevelStarted;
        private OnGameStarted onGameStarted;
        private OnLootChanged onLootChanged;
        private OnDeath onDeath;
        private OnIntroductionClosed onIntroductionClosed;
        private GameState gameState;
        
        [SerializeField] private List<LevelConfig> levelConfigs;
        [SerializeField] private GameObject hudMenu;
        [SerializeField] private GameObject placeMenu;
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject looseMenu;
        [SerializeField] private GameObject endMenu;
        [SerializeField] private LevelScript level;
        [SerializeField] private GameObject player;
        
        private void Start()
        {
            ServiceManager.Instance.Reset();
            onLevelDone = ServiceManager.Instance.Get<OnLevelDone>();
            onShopClosed = ServiceManager.Instance.Get<OnShopClosed>();
            onLevelStarted = ServiceManager.Instance.Get<OnLevelStarted>();
            onGameStarted = ServiceManager.Instance.Get<OnGameStarted>();
            onLootChanged = ServiceManager.Instance.Get<OnLootChanged>();
            onIntroductionClosed = ServiceManager.Instance.Get<OnIntroductionClosed>();
            onDeath = ServiceManager.Instance.Get<OnDeath>();
            
            gameState = ServiceManager.Instance.Get<GameState>();
            onIntroductionClosed.Subscribe(HandleIntroductionClosed);
            onShopClosed.Subscribe(HandlePlacePhase);
            onLevelStarted.Subscribe(HandleOnLevelStarted);
            onLevelDone.Subscribe(HandleLevelDone);
            onLootChanged.Subscribe(HandleOnLootChanged);
            onDeath.Subscribe(HandleOnDeath);
        }

        private void HandleOnDeath(GameObject obj)
        {
            if (obj != player)
            {
                if (obj.TryGetComponent(out MinionScript minion) && minion.allegiance != Allegiance.Ally)
                {
                    gameState.Money += 1;
                }
                Destroy(obj);
                return;
            }
            
            player.SetActive(false);
        }

        private void HandleIntroductionClosed()
        {
            placeMenu.SetActive(true);
            onGameStarted.Invoke();
            gameState.State = GameStateEnum.Placement;
            HandlePlacePhase();
        }

        private void HandleOnLevelStarted()
        {
            gameState.State = GameStateEnum.Level;
            placeMenu.SetActive(false);
            hudMenu.SetActive(true);
        }

        private void HandleOnLootChanged(List<LootScript> obj)
        {
            if (obj.Count <= 0)
            {
                onLevelDone.Invoke(LevelDoneType.Lose);
            }
        }

        private void OnDestroy() 
        {
            onIntroductionClosed.Unsubscribe(HandleIntroductionClosed);
            onShopClosed.Unsubscribe(HandlePlacePhase);
            onLevelStarted.Unsubscribe(HandleOnLevelStarted);
            onLevelDone.Unsubscribe(HandleLevelDone);
            onLootChanged.Unsubscribe(HandleOnLootChanged);
        }

        private void HandleLevelDone(LevelDoneType type)
        {
            player.SetActive(true);
            if (player.TryGetComponent(out LifeScript life)) life.Life = life.maxLife;
            
            hudMenu.SetActive(false);
            Time.timeScale = 0;
            
            if (type == LevelDoneType.Win)
            {
                gameState.Money += gameState.LootCount;
                gameState.State = GameStateEnum.Shop;
                winMenu.SetActive(true);
            }
            else if (type == LevelDoneType.Lose)
            {
                gameState.State = GameStateEnum.GameOver;
                looseMenu.SetActive(true);
            }
        }

        private void HandlePlacePhase()
        {
            levelIndex++;
            if (levelIndex >= levelConfigs.Count)
            {
                gameState.State = GameStateEnum.End;
                endMenu.SetActive(true);
                return;
            }
            var currentLevel = levelConfigs[levelIndex];
        
            level.PrepareLevel(currentLevel);

            if (gameState.CountItems > 0)
            {
                placeMenu.SetActive(true);
            }
            else
            {
                onLevelStarted.Invoke();
            }

            Time.timeScale = 1;
        }
    }
}
