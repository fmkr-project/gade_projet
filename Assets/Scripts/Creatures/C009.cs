using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C009 : BaseCreatureData
    {
        public C009()
        {
            Id = 9;
            Nickname = "BLIBLUB";

            Hp = 65;
            Attack = 125;
            Defense = 55;
            Speed = 30;

            Types = new List<Type> {Type.Psychic};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {1, new Confusion()},
                {12, new Psybeam()},
                {40, new Psychic()}
            };

            CatchRate = 100;
        }
    }
}