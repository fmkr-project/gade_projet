namespace Attacks
{
    public class Swift : Attack
    {
        public new string Name => "METEORES";
        public new Type Type => Type.Normal;
        public new int Power => 60;
        public new int Accuracy => CannotMiss;
    }
}