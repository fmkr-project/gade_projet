namespace Attacks
{
    public record Harden : Attack
    {
        public Harden()
        {
            Name = "ARMURE";
            Type = Type.Normal;
            Accuracy = 100;

            Target = StatusAttackTarget.User;
            DefenseBuff = +1;
        }
    }
}