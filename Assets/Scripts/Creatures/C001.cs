using System.Collections.Generic;
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
        }
    }
}