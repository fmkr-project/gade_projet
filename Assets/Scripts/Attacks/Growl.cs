namespace Attacks
{
    public record Growl : Attack
    {
        public Growl()
        {
            Name = "RUGISSEMENT";
            Type = Type.Normal;
            Accuracy = 100;

            Target = StatusAttackTarget.Enemy;
            AttackBuff = -1;
        }
    }
}