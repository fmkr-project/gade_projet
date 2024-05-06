namespace Attacks
{
    public record Psychic : Attack
    {
        public Psychic()
        {
            Name = "PSYKO";
            Type = Type.Psychic;
            Power = 90;
            Accuracy = 100;
        }
    }
}