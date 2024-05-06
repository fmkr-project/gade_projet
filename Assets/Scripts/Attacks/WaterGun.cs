namespace Attacks
{
    public record WaterGun : Attack
    {
        public WaterGun()
        {
            Name = "PISTOLET À O";
            Type = Type.Water;
            Power = 40;
            Accuracy = 100;
        }
    }
}