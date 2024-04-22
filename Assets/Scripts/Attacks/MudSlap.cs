namespace Attacks
{
    public class MudSlap : Attack
    {
        public new string Name => "COUD'BOUE";
        public new Type Type => Type.Ground;
        public new int Power => 20;
        public new int Accuracy => 100;

        public new StatusAttackTarget Target => StatusAttackTarget.Enemy;
        public new int AccuracyBuff = -1;
    }
}