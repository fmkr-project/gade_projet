namespace Attacks
{
    public class Earthquake : Attack
    {
        public new string Name => "SEISME";
        public new Type Type => Type.Ground;
        public new int Power => 100;
        public new int Accuracy => 100;
    }
}