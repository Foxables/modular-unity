using System;
using UnityEngine;

namespace Core.Entity
{
    class PlayerActorEntity : AbstractActorEntity, PlayerActorEntityInterface
    {
        [Range(0.01f, 100f)]public float BaseCriticalHit = 0.05f;
        [Range(0.01f, 100f)]public float BaseDodgeChance = 0.05f;
        [Range(0.01f, 100f)]public float BaseParrayChance = 0.05f;
        public int BaseDefence = 0;
        public float WeaponMin = 5f;
        public float WeaponMax = 10f;

        protected override double CalculateCriticalStrikeChance()
        {
            return CalculatePercentageFromStat(Agility, BaseCriticalHit);
        }

        protected override double CalculateDodgeChance()
        {
            return CalculatePercentageFromStat(Agility, BaseDodgeChance);
        }

        protected override double CalculateParrayChance()
        {
            return CalculatePercentageFromStat(Agility, BaseParrayChance);
        }

        protected override double CalculateDefence()
        {
            return base.CalculateDefence() + BaseDefence;
        }
    }
}