using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C014 : BaseCreatureData
    {
        public C014()
        {
            Id = 14;
            Nickname = "DOSOLO";

            Hp = 35;
            Attack = 85;
            Defense = 45;
            Speed = 75;

            Types = new List<Type> {Type.Normal, Type.Flying};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Peck()},
                {1, new Growl()},
                {21, new TriAttack()},
                {37, new DrillPeck()},
                {45, new Agility()}
            };

            CatchRate = 45;
        }
    }
}