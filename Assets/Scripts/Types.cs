using System.Collections.Generic;

public enum Type
{
    NeutralType, // Struggle, etc.
    Normal,
    Fire,
    Water,
    Grass,
    Flying,
    Electr,
    Ice,
    
    Ground,
    Psychic,
    Steel
}

public static class TypeToString
{
    public static string Convert(Type type)
    {
        return type switch
        {
            Type.NeutralType => "???",
            Type.Normal => "NORMAL",
            Type.Fire => "FEU",
            Type.Water => "EAU",
            Type.Grass => "HERBE",
            Type.Flying => "VOL",
            Type.Electr => "ELECTRIK",
            Type.Ice => "GLACE",
            Type.Ground => "SOL",
            Type.Psychic => "PSY",
            Type.Steel => "ACIER",
            _ => "/"
        };
    }
}

public class TypeChart
// Documents weaknesses and resistances of types
// List can be empty
{
    public readonly Dictionary<Type, List<Type>> WeakAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},
            
            {Type.Fire, new List<Type> {Type.Water, Type.Ground}},
            {Type.Water, new List<Type> {Type.Grass, Type.Electr}},
            {Type.Grass, new List<Type> {Type.Fire, Type.Ice}},

            {Type.Flying, new List<Type> {Type.Electr, Type.Ice}},
            {Type.Electr, new List<Type> {Type.Ground}},
            {Type.Ice, new List<Type> {Type.Fire, Type.Steel}},
            
            {Type.Ground, new List<Type> {Type.Water, Type.Grass, Type.Ice}},
            {Type.Psychic, new List<Type> {}},
            {Type.Steel, new List<Type> {Type.Fire, Type.Ground}}
        };

    public readonly Dictionary<Type, List<Type>> StrongAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},

            {Type.Fire, new List<Type> {Type.Fire, Type.Grass, Type.Ice}},
            {Type.Water, new List<Type> {Type.Fire, Type.Water, Type.Ice}},
            {Type.Grass, new List<Type> {Type.Water, Type.Grass, Type.Electr, Type.Ground}},

            {Type.Flying, new List<Type> {Type.Grass}},
            {Type.Electr, new List<Type> {Type.Electr}},
            {Type.Ice, new List<Type> {Type.Ice}},
            
            {Type.Ground, new List<Type> {}},
            {Type.Psychic, new List<Type> {Type.Psychic}},
            {Type.Steel, new List<Type> {Type.Normal, Type.Flying, Type.Steel, Type.Grass, Type.Psychic, Type.Ice}
        }
        };

    public readonly Dictionary<Type, List<Type>> ImmuneAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},

            {Type.Fire, new List<Type>()},
            {Type.Water, new List<Type>()},
            {Type.Grass, new List<Type>()},

            {Type.Flying, new List<Type> {Type.Ground}},
            {Type.Electr, new List<Type>()},
            {Type.Ice, new List<Type> {}},
            
            {Type.Ground, new List<Type> {Type.Electr}},
            {Type.Psychic, new List<Type> {}},
            {Type.Steel, new List<Type> {}}
        };
}