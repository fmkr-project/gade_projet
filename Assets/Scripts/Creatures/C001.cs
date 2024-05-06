using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C001 : BaseCreatureData
    {
        public C001()
        {
            Id = 1;
            Nickname = "CYLINDRE";

            Hp = 10;
            Attack = 55;
            Defense = 25;
            Speed = 95;

            Types = new List<Type> {Type.Ground};

            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Tackle()},
                {1, new SandAttack()},
                {5, new Growl()},
                {9, new Magnitude()},
                {25, new MudSlap()},
                {33, new Slash()},
                {41, new Earthquake()},
                {49, new Fissure()}
            };

            CatchRate = 255;
        }
    }
}