namespace Attacks
{
    public record Confusion : Attack
    {
        public Confusion()
        {
            Name = "CHOC MENTAL";
            Type = Type.Psychic;
            Power = 50;
            Accuracy = 100;
        }
    }
}