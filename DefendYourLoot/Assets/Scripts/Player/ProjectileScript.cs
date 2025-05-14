using System;
using System.Linq;
using AI;
using Core;
using Events;
using Level;
using UnityEngine;
using Utility;

namespace Player
{
    public class ProjectileScript : MonoBehaviour {
        public GameObject attackParticle;
        private OnAttacked onAttacked;
        [Header("Stats")]
        public float attackDamage = 1;
        public float attackRange = 1;
        public float lifeTime = 5;
        public GameObject attackSource;

        private void Start()
        {
            onAttacked = ServiceManager.Instance.Get<OnAttacked>();
        }

        private void CheckForAttack() {

            var hit = Physics2D
                .OverlapCircleAll(transform.position, attackRange)
                .FirstOrDefault(x => !x.GetComponent<MoveScript>() && (x.GetComponent<WallScript>() || x.TryGetComponent(out MinionScript minion) && minion.allegiance == Allegiance.Enemy));
        
            if(!hit) return;
            if(hit.TryGetComponent(out LifeScript life)) 
            {
                onAttacked.Invoke(attackSource);
                life.Life -= attackDamage;
                Instantiate(attackParticle, hit.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private void Update() {
            CheckForAttack();
            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0) Destroy(gameObject);
        }
    }
}