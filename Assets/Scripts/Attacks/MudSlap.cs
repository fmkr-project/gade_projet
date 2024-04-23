namespace Attacks
{
    public record MudSlap : Attack
    {
        public MudSlap()
        {
            Name = "COUD'BOUE";
            Type = Type.Ground;
            Power = 20;
            Accuracy = 100;

            Target = StatusAttackTarget.Enemy;
            AccuracyBuff = -1;
        }
    }
}