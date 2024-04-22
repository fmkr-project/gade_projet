namespace Attacks
{
    public class Harden
    {
        public new string Name => "ARMURE";
        public new Type Type => Type.Normal;
        public new int Accuracy => 100;

        public new StatusAttackTarget Target => StatusAttackTarget.User;
        public new int DefenseBuff = +1;
    }
}