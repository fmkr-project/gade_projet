namespace Attacks
{
    public record Derp : Attack
    {
        public Derp()
        {
            Name = "OOF";
            Type = Type.NeutralType;
            Power = OHKO;
            Accuracy = CannotMiss;
        }
    }
}