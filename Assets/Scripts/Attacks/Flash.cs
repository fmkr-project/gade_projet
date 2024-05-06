namespace Attacks
{
    public record Flash : Attack
    // the better one
    {
        public Flash()
        {
            Name = "FLASH";
            Type = Type.Normal;
            Power = 50;
            Accuracy = 95;

            Desc = "Ennemi PRC -1";
            
            Target = StatusAttackTarget.Enemy;
            AccuracyBuff = -1;
        }
    }
}