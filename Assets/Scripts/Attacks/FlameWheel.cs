namespace Attacks
{
    public record FlameWheel : Attack
    {
        public FlameWheel()
        {
            Name = "ROUE DE FEU";
            Type = Type.Fire;
            Power = 60;
            Accuracy = 100;
        }
    }
}