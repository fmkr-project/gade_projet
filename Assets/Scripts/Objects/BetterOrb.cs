namespace Objects
{
    public record BetterOrb : CaptureOrb
    {
        public BetterOrb()
        {
            Name = "ORBE MIEUX";
            Description = "Une orbe de meilleure qualité que l'ORBE USUELLE.";
            CaptureMultiplier = 1.5f;
        }
    }
}