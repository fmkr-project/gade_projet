namespace Attacks
{
    public record Fissure : Attack
    {
        public Fissure()
        {
            Name = "FISSURE";
            Type = Type.Ground;
            Power = OHKO;
            Accuracy = 30;
        }
    }
}