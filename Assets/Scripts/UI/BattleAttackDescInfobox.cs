using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleAttackDescInfobox : MonoBehaviour
    {
        private TextMeshProUGUI _tmObject;

        private void Awake()
        {
            _tmObject = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Redraw(Attack attack)
        {
            _tmObject.text = attack is not null ? $"{attack.Desc}" : "";
        }
    }
}