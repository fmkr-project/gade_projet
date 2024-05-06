namespace Attacks
{
    public record Harden : Attack
    {
        public Harden()
        {
            Name = "ARMURE";
            Type = Type.Normal;
            Accuracy = 100;

            Desc = "DEF +1";
            
            Target = StatusAttackTarget.User;
            DefenseBuff = +1;
        }
    }
}