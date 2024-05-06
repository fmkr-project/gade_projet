namespace Attacks
{
    public record IceBeam : Attack
    {
        public IceBeam()
        {
            Name = "LASER GLACE";
            Type = Type.Ice;
            Power = 90;
            Accuracy = 100;
        }
    }
}