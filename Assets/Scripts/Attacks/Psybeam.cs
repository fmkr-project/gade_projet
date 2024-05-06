namespace Attacks
{
    public record Psybeam : Attack
    {
        public Psybeam()
        {
            Name = "RAFALE PSY";
            Type = Type.Psychic;
            Power = 65;
            Accuracy = 100;
        }
    }
}