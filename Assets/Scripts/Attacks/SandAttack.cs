namespace Attacks
{
    public record SandAttack : Attack
    {
        public SandAttack()
        {
            Name = "JET DE SABLE";
            Type = Type.Ground;
            Accuracy = 100;

            Desc = "Ennemi PRC -1";
            
            Target = StatusAttackTarget.Enemy;
            AccuracyBuff = -1;
        }
    }
}