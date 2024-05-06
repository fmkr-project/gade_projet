using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C002 : BaseCreatureData
    {
        public C002()
        {
            Id = 2;
            Nickname = "UN";

            Hp = 15;
            Attack = 40;
            Defense = 10;
            Speed = 100;

            Types = new List<Type> {Type.Normal};

            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new TarotDraw()},
                {1, new TarotBonk()}
            };

            CatchRate = 255;
        }
    }
}