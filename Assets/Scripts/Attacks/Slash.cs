namespace Attacks
{
    public record Slash : Attack
    {
        public Slash()
        {
            Name = "TRANCHE";
            Type = Type.Normal;
            Power = 70;
            Accuracy = CannotMiss;

            Desc = "Ne rate pas";
        }
    }
}