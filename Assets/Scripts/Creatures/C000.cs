using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class C000 : BaseCreatureData
    // For debug only
    {
        public C000()
        {
            Id = 0;
            Nickname = "??????????";

            Hp = 5;
            Attack = 5;
            Defense = 5;
            Speed = 5;

            Types = new List<Type> {Type.NeutralType};
        }
    }
}