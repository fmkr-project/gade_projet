using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C004 : BaseCreatureData
    {
        public C004()
        {
            Id = 4;
            Nickname = "EXCUSE";

            Hp = 40;
            Attack = 110;
            Defense = 45;
            Speed = 155;

            Types = new List<Type> {Type.Normal};

            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new TarotDraw()},
                {1, new TarotBonk()}
            };

            CatchRate = 45;
        }
    }
}