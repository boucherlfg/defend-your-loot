using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Events;
using Level;
using Pathfinding;
using Player;
using UnityEngine;
using Utility;

namespace AI
{
    public class AIMoveScript : MonoBehaviour
    {
        private static List<Transform> currentTargets = new();
        public bool debug = false;
        private Waypoint[] waypoints;
        private StateDelegate state;
        private AIPath aiPath;
        private MinionScript minion;
        private Vector2 startPosition;

        public IdleType idleType;
        public float followRange = 3;
        public float forgetRange = 5;
        
        private OnAttacked onAttacked;
        
        // Start is called before the first frame update
        private void Start()
        {
            onAttacked = ServiceManager.Instance.Get<OnAttacked>();
            waypoints = FindObjectsOfType<Waypoint>();
            aiPath = GetComponent<AIPath>();
            minion = GetComponent<MinionScript>();
            onAttacked.Subscribe(HandleAttacked);
            startPosition = transform.position;
            aiPath.destination = startPosition;
            state = Roam;
        }

        private void Update()
        {
            var oldState = state;
            state = state();

            if (state == oldState) return;
            if(debug) Debug.Log(state.Method.Name);
        }

        private void OnDestroy() {
            onAttacked.Unsubscribe(HandleAttacked);
        }

        private void HandleAttacked(GameObject source)
        {
            if (!source.TryGetComponent(out MinionScript sourceMinion)) return;
            if (minion.allegiance == sourceMinion.allegiance) return;
            
            state = () => !source ? Roam() : FollowEnemy(source.transform);
        }

        private StateDelegate Roam()
        {
            aiPath.destination = idleType == IdleType.Roam ? waypoints.GetRandom().transform.position : startPosition;
            return RoamSubTask;
            
            StateDelegate RoamSubTask()
            {
                var hit = CloseEnemy;
                if (hit)
                {
                    return () => FollowEnemy(hit.transform);
                }
                
                if (Vector2.Distance(transform.position, aiPath.destination) > 0.1f) return RoamSubTask;
                
                return Roam;
            }
        }
        
        private StateDelegate FollowEnemy(Transform enemyTarget)
        {
            if (!enemyTarget) return Roam;

            aiPath.destination = enemyTarget.position;
            return FollowEnemySubTask;
            
            StateDelegate FollowEnemySubTask()
            {
                if (Vector2.Distance(aiPath.destination, transform.position) > 0.1f)
                {
                    return FollowEnemySubTask;
                }
                return () => FollowEnemy(enemyTarget);
            }
        }

        private Collider2D CloseEnemy
        {
            get
            {
                var hits = new Collider2D[10];
                if (Physics2D.OverlapCircle(transform.position, followRange, new ContactFilter2D().NoFilter(), hits) <= 0)
                {
                    return null;
                }
                
                hits = Array.FindAll(hits, x => x && x.TryGetComponent(out MinionScript m) && m.allegiance != minion.allegiance);

                return hits.OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
                    .FirstOrDefault();
            }
        }
    }
}