namespace Objects
{
    public record SuperPotion : HealingItem
    {
        public SuperPotion()
        {
            Name = "POTION MIEUX";
            Description = "Potion de soin (50 PV)";
            HealedHp = 20;
        }
    }
}