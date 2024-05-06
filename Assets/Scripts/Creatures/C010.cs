using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C010 : BaseCreatureData
    {
        public C010()
        {
            Id = 10;
            Nickname = "BLUBLIBLUB";

            Hp = 110;
            Attack = 125;
            Defense = 80;
            Speed = 30;

            Types = new List<Type> {Type.Psychic};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {1, new Confusion()},
                {12, new Psybeam()},
                {40, new Psychic()},
                {41, new DizzyPunch()}
            };

            CatchRate = 50;
        }
    }
}