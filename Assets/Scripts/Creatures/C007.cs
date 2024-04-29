using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C007 : BaseCreatureData
    {
        public C007()
        {
            Id = 7;
            Nickname = "TROICYLINDRES";

            Hp = 35;
            Attack = 80;
            Defense = 50;
            Speed = 120;

            Types = new List<Type> {Type.Ground};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Tackle()},
                {8, new MudSlap()},
                {22, new Earthquake()}
            };
        }
    }
}