using System.Collections.Generic;

public enum Type
{
    NeutralType, // Struggle, etc.
    Fire,
    Water,
    Grass
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
            
            {Type.Fire, new List<Type> {Type.Water}},
            {Type.Water, new List<Type> {Type.Grass}},
            {Type.Grass, new List<Type> {Type.Fire}}
        };

    public readonly Dictionary<Type, List<Type>> StrongAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Fire, new List<Type> {Type.Grass}},
            {Type.Water, new List<Type> {Type.Fire}},
            {Type.Grass, new List<Type> {Type.Grass}}
        };

    public readonly Dictionary<Type, List<Type>> ImmuneAgainst
        = new Dictionary<Type, List<Type>>()
        {
            {Type.NeutralType, new List<Type>()},
            
            {Type.Fire, new List<Type>()},
            {Type.Water, new List<Type>()},
            {Type.Grass, new List<Type>()}
        };
}