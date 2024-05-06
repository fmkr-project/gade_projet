namespace Attacks
{
    public record TarotBonk : Attack
    {
        public TarotBonk()
        {
            Name = "COIINCHE";
            Type = Type.Normal;
            Power = 60;
            Accuracy = 100;

            Desc = "Ennemi DEF -1";
            
            Target = StatusAttackTarget.Enemy;
            DefenseBuff = -1;
        }
    }
}