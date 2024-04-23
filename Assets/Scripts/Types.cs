using System.Collections.Generic;

public enum Type
{
    NeutralType, // Struggle, etc.
    Normal,
    Fire,
    Water,
    Grass,
    
    Ground
    // todo types ++
}

public class TypeToString
{
    public static string Convert(Type type)
    {
        switch (type)
        {
            case Type.NeutralType: return "???";
            case Type.Normal: return "NORMAL";
            case Type.Fire: return "FEU";
            case Type.Water: return "EAU";
            case Type.Grass: return "HERBE";
            
            case Type.Ground: return "SOL";
            default: return "/";
        }
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
            
            {Type.Fire, new List<Type> {Type.Water}},
            {Type.Water, new List<Type> {Type.Grass}},
            {Type.Grass, new List<Type> {Type.Fire}},
            
            {Type.Ground, new List<Type> {Type.Water, Type.Grass}}
        };

    public readonly Dictionary<Type, List<Type>> StrongAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},

            {Type.Fire, new List<Type> {Type.Grass}},
            {Type.Water, new List<Type> {Type.Fire}},
            {Type.Grass, new List<Type> {Type.Grass}},
            
            {Type.Ground, new List<Type> {}}
        };

    public readonly Dictionary<Type, List<Type>> ImmuneAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},

            {Type.Fire, new List<Type>()},
            {Type.Water, new List<Type>()},
            {Type.Grass, new List<Type>()},
            
            {Type.Ground, new List<Type> {}}
        };
}