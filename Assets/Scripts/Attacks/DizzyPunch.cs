namespace Attacks
{
    public record DizzyPunch : Attack
    {
        public DizzyPunch()
        {
            Name = "UPPERCUT";
            Type = Type.Normal;
            Power = 70;
            Accuracy = 100;

            Desc = "Ennemi DEF -2";
            
            Target = StatusAttackTarget.Enemy;
            DefenseBuff = -2;
        }
    }
}