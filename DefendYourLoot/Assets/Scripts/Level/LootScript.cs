using System.Collections.Generic;
using Core;
using Events;
using Managers;
using UnityEngine;
using Utility;

namespace Level
{
    public class LootScript : MonoBehaviour
    {
        private GameState gameState;
        // Start is called before the first frame update
        private void Start()
        {
            gameState = ServiceManager.Instance.Get<GameState>();
            gameState.AddLoot(this);
        }

        private void OnDestroy() {
            gameState.RemoveLoot(this);
        }

    }
}
