using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C003 : BaseCreatureData
    {
        public C003()
        {
            Id = 3;
            Nickname = "VINGT-ET-UN";

            Hp = 25;
            Attack = 75;
            Defense = 25;
            Speed = 125;

            Types = new List<Type> {Type.Normal};

            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new TarotDraw()},
                {1, new TarotBonk()}
            };

            CatchRate = 100;
        }
    }
}