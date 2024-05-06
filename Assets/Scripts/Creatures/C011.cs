using System.Collections.Generic;
using Attacks;
using UnityEngine;

namespace Creatures
{
    public class C011 : BaseCreatureData
    {
        public C011()
        {
            Id = 11;
            Nickname = "TEORIDUKOMPLO";

            Hp = 120;
            Attack = 200;
            Defense = 100;
            Speed = 15;

            Types = new List<Type> {Type.Fire};
            
            LearnableAttacks = new Dictionary<int, Attack>
            {
                {0, new Flamethrower()},
                {1, new Hydropump()}
            };

            CatchRate = 10;
        }
    }
}