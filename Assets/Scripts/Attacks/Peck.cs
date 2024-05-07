namespace Attacks
{
    public record Peck : Attack
    {
        public Peck()
        {
            Name = "PICPIC";
            Type = Type.Flying;
            Power = 35;
            Accuracy = 100;
        }
    }
}