namespace Objects
{
    public record Potion : HealingItem
    {
        public Potion()
        {
            Name = "POTION";
            Description = "Potion de soin (50 PV)";
            HealedHp = 50;
        }
    }
}