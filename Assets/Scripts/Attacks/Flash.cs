namespace Attacks
{
    public class Flash
    // the better one
    {
        public new string Name => "FLASH";
        public new Type Type => Type.Normal;
        public new int Power => 50;
        public new int Accuracy => 95;

        public new StatusAttackTarget Target => StatusAttackTarget.Enemy;
        public new int AccuracyBuff = -1;
    }
}