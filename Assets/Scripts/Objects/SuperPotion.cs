namespace Objects
{
    public record SuperPotion : HealingItem
    {
        public SuperPotion()
        {
            Name = "POTION MIEUX";
            Description = "Potion de soin (100 PV)";
            HealedHp = 1000;
        }
    }
}