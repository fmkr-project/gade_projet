namespace Attacks
{
    public record Swift : Attack
    {
        public Swift()
        {
            Name = "METEORES";
            Type = Type.Normal;
            Power = 60;
            Accuracy = CannotMiss;
        }
    }
}