using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class C006 : BaseCreatureData
    {
        public C006()
        {
            Id = 6;
            Nickname = "DEUXCYLINDRES";

            Hp = 20;
            Attack = 65;
            Defense = 35;
            Speed = 110;

            Types = new List<Type> {Type.Fire};
        }
    }
}