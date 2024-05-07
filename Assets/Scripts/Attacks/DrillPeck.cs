namespace Attacks
{
    public record DrillPeck : Attack
    {
        public DrillPeck()
        {
            Name = "BEC VRILLE";
            Type = Type.Flying;
            Power = 80;
            Accuracy = 100;
        }
    }
}