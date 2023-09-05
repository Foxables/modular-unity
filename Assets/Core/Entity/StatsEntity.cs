namespace Core.Entity
{
    class StatsEntity : AbstractEntity, EntityInterface
    {
        int Strength { get; set; }
        int Stamina { get; set; }
        int Agility { get; set; }
        int Intelligence { get; set; }
        int Spirit { get; set; }
    }
}
