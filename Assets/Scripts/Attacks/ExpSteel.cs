namespace Attacks
{
    public record ExpSteel : Attack
    {
        public ExpSteel()
        {
            Name = "EXP:Z291R";
            Type = Type.Steel;
            Power = 70;
            Accuracy = 100;

            Desc = "Ennemi VIT -1";
            Target = StatusAttackTarget.Enemy;
            SpeedBuff = -1;
        }
    }
}