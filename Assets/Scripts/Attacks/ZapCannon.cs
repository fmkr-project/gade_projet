namespace Attacks
{
    public record ZapCannon : Attack
    {
        public ZapCannon()
        {
            Name = "ÉLECANON";
            Type = Type.Electr;
            Power = 100;
            Accuracy = 75; // Lock-On not implemented
        }
    }
}