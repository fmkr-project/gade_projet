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

            Desc = "Ennemi PRC -1";
            
            Target = StatusAttackTarget.Enemy;
            AccuracyBuff = -1;
        }
    }
}