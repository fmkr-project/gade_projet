namespace Objects
{
    public record BetterBetterOrb : CaptureOrb
    {
        public BetterBetterOrb()
        {
            Name = "ORBE MIEUX";
            Description = "Une orbe de meilleure qualité que l'ORBE USUELLE.";
            CaptureMultiplier = 2f;
        }
    }
}