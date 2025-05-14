using System.Collections.Generic;
using System.Linq;
using AI;
using Config;
using Core;
using Events;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Level
{
    public class LevelScript : MonoBehaviour
    {
        private List<GameObject> heroInstances = new();
        private float timer;
        private float spawnInterval; 
        private List<GameObject> heroes = new();
        private HeroSpawner[] heroSpawners;
        private GameState gameState;
        private OnDeath onDeath;
        private OnLevelDone onLevelDone;
    
        private void Start() {
            AstarPath.active.Scan();
            gameState = ServiceManager.Instance.Get<GameState>();
            onLevelDone = ServiceManager.Instance.Get<OnLevelDone>();
            heroSpawners = FindObjectsOfType<HeroSpawner>();
            
        }

        private void Update()
        {
            if (gameState.State != GameStateEnum.Level) return;
            
            heroInstances.RemoveAll(x => !x);

            if (heroInstances.Count <= 0 && heroes.Count <= 0)
            {
                onLevelDone.Invoke(LevelDoneType.Win);
            }
            
            if (gameState.State != GameStateEnum.Level) return;
            if (!heroes.Any()) return;
            timer += Time.deltaTime;
            if (timer <= spawnInterval) return;

            timer = 0;
            var spawner = heroSpawners.GetRandom();
            var hero = heroes[0];
            heroes.RemoveAt(0);
            heroInstances.Add(Instantiate(hero, spawner.transform.position, Quaternion.identity, transform));
        }

        public void PrepareLevel(LevelConfig levelConfig)
        {
            heroes.AddRange(levelConfig.heroes);
            spawnInterval = levelConfig.spawnInterval;
            timer = 0;
        }
    }
}