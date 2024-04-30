namespace Attacks
{
    public record Magnitude : Attack
    {
        public Magnitude()
        {
            Name = "MAGNITUDE";
            Type = Type.Ground;
            Power = 50;
            Accuracy = 100;
        }
    }
}