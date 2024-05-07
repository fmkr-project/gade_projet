namespace Attacks
{
    public record ExpElec : Attack
    {
        public ExpElec()
        {
            Name = "EXP:74H6X";
            Type = Type.Electr;
            Power = 70;
            Accuracy = 100;
            
            Desc = "Ennemi VIT -1";
            Target = StatusAttackTarget.Enemy;
            SpeedBuff = -1;
        }
    }
}