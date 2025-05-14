using System;
using Player;
using UnityEngine;

namespace Level
{
    public class ManaRegen : EffectInZone
    {
        protected override void Affect(Collider2D hit, ref bool shouldRecharge)
        {
            if (!hit.TryGetComponent<PlayerAttackScript>(out var player)) return;

            if (IsCharged)
            {
                player.Mana += chargeSpeed * Time.deltaTime;
                currentChargeTime += Time.deltaTime;
            }

            shouldRecharge = false;
        }
    }
}
