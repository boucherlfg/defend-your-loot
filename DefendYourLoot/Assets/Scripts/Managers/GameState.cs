using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Events;
using Level;
using Utility;

namespace Managers
{
    public class GameState
    {
        private readonly OnMoneyChanged onMoneyChanged = ServiceManager.Instance.Get<OnMoneyChanged>();
        private readonly OnInventoryChanged onInventoryChanged = ServiceManager.Instance.Get<OnInventoryChanged>();
        private readonly OnGameStateChanged onGameStateChanged = ServiceManager.Instance.Get<OnGameStateChanged>();
        private readonly OnLootChanged onLootChanged = ServiceManager.Instance.Get<OnLootChanged>();
        
        private readonly List<PlaceableItem> inventory = new();
        private readonly List<LootScript> loots = new();
        private GameStateEnum state = GameStateEnum.Intro;
        private int money = 0;

        public GameStateEnum State
        {
            get => state;
            set
            {
                state = value;
                onGameStateChanged.Invoke(state);
            }
        }
        public int Money
        {
            get => money;
            set
            {
                money = value;
                onMoneyChanged.Invoke(money);
            }
        }
        
        public void AddLoot(LootScript loot)
        {
            loots.Add(loot);
            onLootChanged.Invoke(loots.ToList());
        }
        public void RemoveLoot(LootScript loot)
        {
            loots.Remove(loot);
            onLootChanged.Invoke(loots.ToList());
        }
        
        public int LootCount => loots.Count;

        public void AddItem(PlaceableItem item)
        {
            inventory.Add(item);
            onInventoryChanged.Invoke(inventory.ToList());
        }

        public void RemoveItem(PlaceableItem item)
        {
            inventory.Remove(item);
            onInventoryChanged.Invoke(inventory.ToList());
        }

        public PlaceableItem GetFirstItem()
        {
            return inventory.FirstOrDefault();
        }
        
        public int CountItems => inventory.Count;


    }
}