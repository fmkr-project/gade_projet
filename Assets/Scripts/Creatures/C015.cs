using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C015 : BaseCreatureData
    {
        public C015()
        {
            Id = 15;
            Nickname = "PTIBATON";

            Hp = 10;
            Attack = 140;
            Defense = 25;
            Speed = 120;

            Types = new List<Type> {Type.Fire, Type.Flying};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Seek()},
                {1, new Agility()}
            };

            CatchRate = 90;
        }
    }
}