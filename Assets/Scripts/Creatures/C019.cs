using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C019 : BaseCreatureData
    {
        public C019()
        {
            Id = 19;
            Nickname = "PIKAGLACE";

            Hp = 45;
            Attack = 75;
            Defense = 45;
            Speed = 40;

            Types = new List<Type> {Type.Steel, Type.Ice};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Tackle()},
                {1, new Harden()},
                {15, new IceBeam()}
            };

            CatchRate = 80;
        }
    }
}