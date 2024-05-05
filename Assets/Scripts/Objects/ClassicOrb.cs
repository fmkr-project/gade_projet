namespace Objects
{
    public record ClassicOrb : CaptureOrb
    {
        public ClassicOrb()
        {
            Name = "ORBE USUELLE";
            Description = "Une orbe pour capturer des Créatures. Efficacité limitée.";
            CaptureMultiplier = 1f;
        }
    }
}