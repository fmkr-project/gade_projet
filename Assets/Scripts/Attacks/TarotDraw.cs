namespace Attacks
{
    public record TarotDraw : Attack
    {
        public TarotDraw()
        {
            Name = "PIOCHE";
            Type = Type.Normal;
            Accuracy = 100;

            Desc = "ATK +1, DEF +1, VIT +1";
            
            Target = StatusAttackTarget.User;
            AttackBuff = +1;
            DefenseBuff = +1;
            SpeedBuff = +1;
        }
    }
}