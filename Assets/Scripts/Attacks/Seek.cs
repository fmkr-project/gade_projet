namespace Attacks
{
    public record Seek : Attack
    {
        public Seek()
        {
            Name = "VERROU";
            Type = Type.Fire;
            Power = 130;
            Accuracy = 55;

            Desc = "Ennemi DEF -1";
            Target = StatusAttackTarget.Enemy;
            DefenseBuff = -1;
        }
    }
}