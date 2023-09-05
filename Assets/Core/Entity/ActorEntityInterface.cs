namespace Core.Entity
{
    interface ActorEntityInterface : EntityInterface
    {
        // Getters;
        public string GetName();
        public int GetStrength();
        public int GetStamina();
        public int GetAgility();
        public int GetIntelligence();
        public int GetSpirit();
        public int GetHealth();
        public int GetMana();
        public int GetMaxHealth();
        public int GetMaxMana();
        public int GetDefence();
        public int GetAttack(int weaponMin, int weaponMax);
        public double GetCriticalStrikeChance();
        public double GetDodge();
        public double GetParray();

        // Setters;
        public ActorEntityInterface SetName(string name);

        // Helpers;
        public bool CanCastSpell(int manaCost);
        public bool CanCastSpell(int manaCost, int healthCost);
        public bool CanBeHealed(int amount);
        public int ModifyHealth(int amount);
        public int ModifyMana(int amount);
        public bool IsAlive();
    }
}