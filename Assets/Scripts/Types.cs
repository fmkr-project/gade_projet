using System.Collections.Generic;

public enum Type
{
    NeutralType, // Struggle, etc.
    Normal,
    Fire,
    Water,
    Grass,
    Electr,
    Ice,
    
    Ground,
    Psychic
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
            
            case Type.Electr: return "ELECTRIK";
            case Type.Ice: return "GLACE";
            
            case Type.Ground: return "SOL";
            case Type.Psychic: return "PSY";
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
            
            {Type.Fire, new List<Type> {Type.Water, Type.Ground}},
            {Type.Water, new List<Type> {Type.Grass, Type.Electr}},
            {Type.Grass, new List<Type> {Type.Fire, Type.Ice}},

            {Type.Electr, new List<Type> {Type.Ground}},
            {Type.Ice, new List<Type> {Type.Fire}},
            
            {Type.Ground, new List<Type> {Type.Water, Type.Grass, Type.Ice}},
            {Type.Psychic, new List<Type> {}}
        };

    public readonly Dictionary<Type, List<Type>> StrongAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},

            {Type.Fire, new List<Type> {Type.Fire, Type.Grass, Type.Ice}},
            {Type.Water, new List<Type> {Type.Fire, Type.Water, Type.Ice}},
            {Type.Grass, new List<Type> {Type.Water, Type.Grass, Type.Electr, Type.Ground}},

            {Type.Electr, new List<Type> {Type.Electr}},
            
            {Type.Ground, new List<Type> {}},
            {Type.Psychic, new List<Type> {Type.Psychic}}
        };

    public readonly Dictionary<Type, List<Type>> ImmuneAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Normal, new List<Type>()},

            {Type.Fire, new List<Type>()},
            {Type.Water, new List<Type>()},
            {Type.Grass, new List<Type>()},

            {Type.Electr, new List<Type>()},
            
            {Type.Ground, new List<Type> {Type.Electr}},
            {Type.Psychic, new List<Type> {}}
        };
}