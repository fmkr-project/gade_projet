namespace Attacks
{
    public record Earthquake : Attack
    {
        public Earthquake()
        {
            Name = "SEISME";
            Type = Type.Ground;
            Power = 100;
            Accuracy = 100;
        }
    }
}