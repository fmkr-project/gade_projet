namespace Objects
{
    public record Potion : HealingItem
    {
        public Potion()
        {
            Name = "POTION";
            Description = "Potion de soin (20 PV)";
            HealedHp = 20;
        }
    }
}