using System.Collections.Generic;
using UnityEngine;

namespace Creatures
{
    public class C001 : BaseCreatureData
    {
        public new int Id = 1;
        public new string Nickname = "feur";
        public new GameObject BattleSprite;

        public new int Hp = 10;
        public new int Attack = 55;
        public new int Defense = 25;
        public new int Speed = 95;

        public new List<Type> Types = new List<Type> {Type.Fire};
    }
}