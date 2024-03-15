public abstract class Attack
{
    // Attack that cannot miss (+ status)
    protected const int CannotMiss = int.MaxValue;
    // One-hit KO
    protected const int OHKO = int.MaxValue;
    
    public string Name;
    public Type Type;
    public int Power;
    public int Accuracy;
}