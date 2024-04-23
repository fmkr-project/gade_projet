namespace Attacks
{
    public record Tackle : Attack
    {
        public Tackle()
        {
            Name = "CHARGE";
            Type = Type.Normal;
            Power = 35;
            Accuracy = 95;
        }
    }
}