namespace Attacks
{
    public record Flamethrower : Attack
    {
        public Flamethrower()
        {
            Name = "LANCE-FLAMMES";
            Type = Type.Fire;
            Power = 90;
            Accuracy = 100;
        }
    }
}