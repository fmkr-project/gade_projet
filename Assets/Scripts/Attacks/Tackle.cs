namespace Attacks
{
    public class Tackle : Attack
    {
        public new string Name => "CHARGE";
        public new Type Type => Type.NeutralType;
        public new int Power => 35;
        public new int Accuracy => 95;
    }
}