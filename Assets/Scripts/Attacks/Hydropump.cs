namespace Attacks
{
    public record Hydropump : Attack
    {
        public Hydropump()
        {
            Name = "HYDROCANON";
            Type = Type.Water;
            Power = 110;
            Accuracy = 80;
        }
    }
}