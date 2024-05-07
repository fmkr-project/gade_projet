namespace Attacks
{
    public record Agility : Attack
    {
        public Agility()
        {
            Name = "HÂTE";
            Type = Type.Flying;
            Accuracy = 100;

            Desc = "VIT +2";
            
            Target = StatusAttackTarget.User;
            SpeedBuff = +2;
        }
    }
}