using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class C000 : BaseCreatureData
    // For debug only
    {
        public new int Id = 0;
        public new GameObject BattleSprite;

        public new int Hp = 5;
        public new int Attack = 5;
        public new int Defense = 5;
        public new int Speed = 5;

        public new List<Type> Types = new List<Type> {Type.NeutralType};
    }
}