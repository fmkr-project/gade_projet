namespace Attacks
{
    public record Thunder : Attack
    {
        public Thunder()
        {
            Name = "TONNERRE";
            Type = Type.Electr;
            Power = 120;
            Accuracy = 70;
        }
    }
}