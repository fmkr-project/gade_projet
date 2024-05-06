namespace Attacks
{
    public record TriAttack : Attack
    {
        public TriAttack()
        {
            Name = "TRIPLATTAQUE";
            Type = Type.Normal;
            Power = 80;
            Accuracy = 100;
        }
    }
}