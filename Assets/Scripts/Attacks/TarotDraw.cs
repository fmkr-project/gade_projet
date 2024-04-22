namespace Attacks
{
    public class TarotDraw
    {
        public new string Name => "PIOCHE";
        public new Type Type => Type.Normal;
        public new int Accuracy => 100;

        public new StatusAttackTarget Target => StatusAttackTarget.User;
        public new int AttackBuff = +1;
        public new int DefenseBuff = +1;
        public new int SpeedBuff = +1;
    }
}