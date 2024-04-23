namespace Attacks
{
    public record TarotEnds : Attack
    {
        public TarotEnds()
        {
            Name = "TRI-BOUT";
            Type = Type.Normal;
            Power = 90;
            Accuracy = 100;
        }
    }
}