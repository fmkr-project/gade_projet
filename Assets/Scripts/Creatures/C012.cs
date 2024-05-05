using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C012 : BaseCreatureData
    {
        public C012()
        {
            Id = 12;
            Nickname = "ORROBOI";

            Hp = 255;
            Attack = 255;
            Defense = 255;
            Speed = 255;

            Types = new List<Type> {Type.Normal};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Derp()}
            };

            CatchRate = 10;
        }
    }
}