namespace Objects
{
    public record ExtraOrb : CaptureOrb
    {
        public ExtraOrb()
        {
            Name = "ORBE ZÉRO";
            Description = "L'orbe des Grands Maîtres. Capture à coup sûr.";
            CaptureMultiplier = 255f;
        }
    }
}