using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class C007 : BaseCreatureData
    {
        public C007()
        {
            Id = 1;
            Nickname = "TROICYLINDRES";

            Hp = 35;
            Attack = 80;
            Defense = 50;
            Speed = 120;

            Types = new List<Type> {Type.Fire};
        }
    }
}