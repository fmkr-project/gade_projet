public enum StatusAttackTarget // a normal attack always targets the enemy
{
    User,
    Enemy
}

public abstract class Attack
{
    // Attack that cannot miss (+ status)
    protected const int CannotMiss = int.MaxValue;
    // One-hit KO
    protected const int OHKO = int.MaxValue;
    
    public readonly string Name = "??????????"; // Placeholder in case the name is not implemented
    public readonly Type Type = Type.NeutralType;
    public readonly int Power;
    public readonly int Accuracy = CannotMiss;
    
    // Status alts
    public readonly StatusAttackTarget Target;
    // TODO status alterations
    public readonly int AttackBuff;
    public readonly int DefenseBuff;
    public readonly int SpeedBuff;
    public readonly int AccuracyBuff;
}