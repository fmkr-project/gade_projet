namespace Attacks
{
    public class Flamethrower : Attack
    {
        public new string Name => "LANCE-FLAMMES";
        public new Type Type => Type.Fire;
        public new int Power => 90;
        public new int Accuracy => 100;
    }
}