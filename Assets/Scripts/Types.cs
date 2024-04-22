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