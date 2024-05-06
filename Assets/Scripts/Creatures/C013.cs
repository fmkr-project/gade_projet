using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C013 : BaseCreatureData
    {
        public C013()
        {
            Id = 13;
            Nickname = "BLEUTRUK";

            Hp = 50;
            Attack = 70;
            Defense = 50;
            Speed = 40;

            Types = new List<Type> {Type.Water};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Tackle()},
                {1, new Growl()},
                {6, new MudSlap()},
                {10, new WaterGun()},
                {42, new Hydropump()}
            };

            CatchRate = 45;
        }
    }
}