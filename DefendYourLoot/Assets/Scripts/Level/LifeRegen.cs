using AI;
using Player;
using UnityEngine;
using Utility;

namespace Level
{
    public class LifeRegen : EffectInZone
    {
        protected override void Affect(Collider2D hit, ref bool shouldRecharge)
        {
            if (!hit.TryGetComponent<LifeScript>(out var ally)) return;
            if (ally.TryGetComponent(out MinionScript minion) && minion.allegiance != Allegiance.Ally) return;
            
            if (IsCharged)
            {
                ally.Life += chargeSpeed * Time.deltaTime;
                currentChargeTime += Time.deltaTime;
            }

            shouldRecharge = false;
        }
    }
}