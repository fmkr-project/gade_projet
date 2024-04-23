namespace Attacks
{
    public record Struggle : Attack
    {
        public Struggle()
        {
            Name = "LUTTE";
            Type = Type.NeutralType;
            Power = 10;
            Accuracy = 100;

            Target = StatusAttackTarget.User;
            DefenseBuff = -1;
        }
    }
}