using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C018 : BaseCreatureData
    {
        public C018()
        {
            Id = 18;
            Nickname = "PROJECT:HZ";

            Hp = 100;
            Attack = 40;
            Defense = 130;
            Speed = 25;

            Types = new List<Type> {Type.Electr};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new ExpElec()}
            };

            CatchRate = 65;
        }
    }
}