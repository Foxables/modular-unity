
using System;

namespace Core.Entity {
    class AbstractActorEntity : StatsEntity, ActorEntityInterface
    {
        public string Name = "";
        public int Strength = 0;
        public int Stamina = 0;
        public int Agility = 0;
        public int Intelligence = 0;
        public int Spirit = 0;
        public int Health = 0;
        public int Mana = 0;
        public int MaxHealth = 0;
        public int MaxMana = 0;
        public int Level = 0;

        public int ModifyHealth(int amount) {
            if (HealthWouldBeGreaterThanMaxHealth(amount)) {
                Health = MaxHealth;
                return Health;
            }

            Health = Math.Max(Health + amount, 0);
            return Health;
        }

        public int ModifyMana(int amount) {
            if (ManaWouldBeGreaterThanMaxMana(amount)) {
                Mana = MaxMana;
                return Mana;
            }

            Mana = Math.Max(Mana + amount, 0);
            return Mana;
        }

        public bool CanCastSpell(int manaCost) {
            return ManaGreaterThanZero() && ManaWouldBeGreaterThanZero(manaCost);
        }

        public bool CanCastSpell(int manaCost, int healthCost) {
            return ManaGreaterThanZero() && ManaWouldBeGreaterThanZero(manaCost) && HealthGreaterThanZero() && HealthWouldBeGreaterThanZero(healthCost);
        }

        public bool CanBeHealed(int amount) {
            return HealthLessThanMaxHealth() && HealthWouldBeLessThanMaxHealth(amount);
        }

        public bool IsAlive() {
            return HealthGreaterThanZero();
        }

        public string GetName() {
            return Name;
        }

        public int GetStrength() {
            return Strength;
        }

        public int GetStamina() {
            return Stamina;
        }

        public int GetAgility() {
            return Agility;
        }

        public int GetIntelligence() {
            return Intelligence;
        }

        public int GetSpirit() {
            return Spirit;
        }

        public int GetHealth() {
            return Health;
        }

        public int GetMana() {
            return Mana;
        }

        public int GetMaxHealth() {
            return MaxHealth;
        }

        public int GetMaxMana() {
            return MaxMana;
        }

        public int GetDefence() {
            return (int)Math.Round(CalculateDefence(), 0, MidpointRounding.AwayFromZero);
        }

        public int GetAttack(int weaponMin, int weaponMax) {
            Random r = new();

            double attack = r.Next(CalculateAttack(weaponMin), CalculateAttack(weaponMax));
            return (int)Math.Round(attack, 0, MidpointRounding.AwayFromZero);
        }

        public double GetCriticalStrikeChance() {
            return CalculateCriticalStrikeChance();
        }

        public double GetDodge() {
            return CalculateDodgeChance();
        }

        public double GetParray() {
            return CalculateParrayChance();
        }

        public ActorEntityInterface SetName(string name) {
            Name = name;
            return this;
        }

        protected virtual double CalculatePercentageFromStat(int stat, float baseStat)
        {
            float Additional = (stat / 4) * Math.Abs((float)Math.Cos(Math.Log(Math.Pow(Level + 10, 2), 2)));
            double result = (Math.Round(baseStat, 2) + Math.Round(Additional / 100f, 2)) / 100f;

            return Math.Min(result, 1.00);
        }

        protected virtual double CalculateCriticalStrikeChance()
        {
            return CalculatePercentageFromStat(Agility, 0);
        }

        protected virtual double CalculateDodgeChance()
        {
            return CalculatePercentageFromStat(Agility, 0);
        }

        protected virtual double CalculateParrayChance()
        {
            return CalculatePercentageFromStat(Agility, 0);
        }

        protected virtual double CalculateDefence() {
            return (Strength / 2 + Stamina / 5) * Math.Log(Math.Pow(Level + 2, 2)) * Math.Log(Strength * Math.E);
        }

        protected virtual int CalculateAttack(int weapon) {
            double attack = (Strength / 2) * Math.Log(Math.Pow(Level + 2, 2)) * Math.Log(Strength * Math.E) + weapon;
            return (int)Math.Round(attack, 0, MidpointRounding.AwayFromZero);
        }

        private bool HealthGreaterThanZero() {
            return Health > 0;
        }

        private bool HealthWouldBeGreaterThanZero(int amount) {
            return Health + amount > 0;
        }

        private bool HealthLessThanMaxHealth() {
            return Health < MaxHealth;
        }

        private bool HealthWouldBeGreaterThanMaxHealth(int amount) {
            return Health + amount > MaxHealth;
        }

        private bool HealthWouldBeLessThanMaxHealth(int amount) {
            return Health + amount < MaxHealth;
        }

        private bool HealthWouldBeLessThanZero(int amount) {
            return Health + amount < 0;
        }

        private bool ManaLessThanMaxMana() {
            return Mana < MaxMana;
        }

        private bool ManaWouldBeLessThanMaxMana(int amount) {
            return Mana + amount < MaxMana;
        }

        private bool ManaGreaterThanZero() {
            return Mana > 0;
        }

        private bool ManaWouldBeGreaterThanZero(int amount) {
            return Mana + amount > 0;
        }

        private bool ManaWouldBeLessThanZero(int amount) {
            return Mana + amount < 0;
        }

        private bool ManaWouldBeGreaterThanMaxMana(int amount) {
            return Mana + amount > MaxMana;
        }
    }
}
