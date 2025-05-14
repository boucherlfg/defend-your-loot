using System;
using Player;
using UnityEngine;

namespace Level
{
    public class EffectInZone : MonoBehaviour
    {
        protected float currentChargeTime;
        [SerializeField] private Color chargedColor = Color.blue;
        [SerializeField] private Color unchargedColor = new Color(0.15f, 0.15f, 0.5f, 1);
        [SerializeField] private float chargeTime = 3;
        [SerializeField] protected float chargeSpeed = 1;
        [SerializeField] protected float range = 1;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public bool IsCharged => currentChargeTime < chargeTime;
        private void OnValidate()
        {
            if(!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            spriteRenderer.color = IsCharged ? chargedColor : unchargedColor;
            
            var hits = new Collider2D[10];
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, range, hits);
            var shouldRecharge = true;
            for (var i = 0; i < count; i++)
            {
                var hit = hits[i];
                Affect(hit, ref shouldRecharge);
            }

            if (!shouldRecharge) return;
            
            chargeTime -= Time.deltaTime;
        }

        protected virtual void Affect(Collider2D hit, ref bool shouldRecharge)
        {
            
        }
    }
}