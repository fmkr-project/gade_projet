public enum StatusAttackTarget // a normal attack always targets the enemy
{
    Nil, // default value (initialized)
    User,
    Enemy
}

public abstract record Attack
{
    // Attack that cannot miss (+ status)
    public static int CannotMiss = int.MaxValue;
    // One-hit KO
    public static int OHKO = int.MaxValue;
    
    public string Name = "??????????"; // Placeholder in case the name is not implemented
    public string Desc = "";
    public Type Type = Type.NeutralType;
    public int Power;
    public int Accuracy = CannotMiss;

    public int Priority;
    
    // Status alts
    public StatusAttackTarget Target = StatusAttackTarget.Nil;
    // TODO status alterations
    public int AttackBuff;
    public int DefenseBuff;
    public int SpeedBuff;
    public int AccuracyBuff;
}