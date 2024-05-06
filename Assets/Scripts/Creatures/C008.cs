using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C008 : BaseCreatureData
    {
        public C008()
        {
            Id = 8;
            Nickname = "BLUB";

            Hp = 45;
            Attack = 105;
            Defense = 45;
            Speed = 20;

            Types = new List<Type> {Type.Psychic};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {1, new Confusion()},
                {12, new Psybeam()},
                {36, new Psychic()}
            };

            CatchRate = 200;
        }
    }
}