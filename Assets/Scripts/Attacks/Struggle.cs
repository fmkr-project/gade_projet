namespace Attacks
{
    public class Struggle : Attack
    {
        public new string Name => "LUTTE";
        public new Type Type => Type.NeutralType;
        public new int Power => 10;
        public new int Accuracy => 100;

        public new StatusAttackTarget Target = StatusAttackTarget.User;
        public new int DefenseBuff = -1;
    }
}