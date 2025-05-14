using System;
using System.Linq;
using Core;
using Events;
using Player;
using UnityEngine;
using Utility;

namespace AI
{
    public class AttackScript : MonoBehaviour
    {
        private StateDelegate state;
        public GameObject attackParticle;
        private AIMoveScript move;
        private MinionScript minion;
    

        [Header("Stats")]
        public float attackDamage = 1;
        public float attackRange = 1;
        public float attackCooldown = 1;
        private float attackTimer;

        void Start()
        {
            attackTimer = attackCooldown;
            move = GetComponent<AIMoveScript>();
            minion = GetComponent<MinionScript>();
            attackTimer = attackCooldown;
            state = AttackWhenTimerIsOver;
        }


        private void Update()
        {
            state = state();
        }

        private StateDelegate AttackWhenTimerIsOver()
        {
            attackTimer += Time.deltaTime;
            var target = CloseEnemy;
            if (attackTimer < attackCooldown) return AttackWhenTimerIsOver;
            if (!target) return AttackWhenTimerIsOver;
            if(!target.TryGetComponent<LifeScript>(out var lifeScript)) return AttackWhenTimerIsOver;
            
            attackTimer = 0;
            
            lifeScript.Life -= attackDamage;
            
            ServiceManager.Instance.Get<OnAttacked>().Invoke(gameObject);
            Instantiate(attackParticle, target.transform.position, Quaternion.identity);
            return AttackWhenTimerIsOver;
        }
        
        private Collider2D CloseEnemy
        {
            get
            {
                var hits = new Collider2D[10];
                if (Physics2D.OverlapCircle(transform.position, attackRange, new ContactFilter2D().NoFilter(), hits) <= 0)
                {
                    return null;
                }
                return hits.Where(x => x && x.TryGetComponent(out MinionScript m) && m.allegiance != minion.allegiance)
                    .OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
                    .FirstOrDefault();
            }
        }
    }
}