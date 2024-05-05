using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C006 : BaseCreatureData
    {
        public C006()
        {
            Id = 6;
            Nickname = "DEUXCYLINDRES";

            Hp = 20;
            Attack = 65;
            Defense = 35;
            Speed = 110;

            Types = new List<Type> {Type.Ground};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Tackle()},
                {9, new Magnitude()},
                {25, new MudSlap()},
                {36, new Slash()},
                {47, new Earthquake()}
            };

            CatchRate = 100;
        }
    }
}