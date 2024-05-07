using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C017 : BaseCreatureData
    {
        public C017()
        {
            Id = 17;
            Nickname = "PROJECT:FE";

            Hp = 100;
            Attack = 40;
            Defense = 130;
            Speed = 25;

            Types = new List<Type> {Type.Steel};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new ExpSteel()}
            };

            CatchRate = 65;
        }
    }
}