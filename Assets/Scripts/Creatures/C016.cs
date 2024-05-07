using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C016 : BaseCreatureData
    {
        public C016()
        {
            Id = 16;
            Nickname = "GROBATON";

            Hp = 75;
            Attack = 105;
            Defense = 100;
            Speed = 80;

            Types = new List<Type> {Type.Steel, Type.Flying};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Seek()},
                {1, new Agility()}
            };

            CatchRate = 40;
        }
    }
}