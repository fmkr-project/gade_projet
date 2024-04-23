using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleAttackSpecInfobox : MonoBehaviour
    {
        private TextMeshProUGUI _tmObject;

        private void Awake()
        {
            _tmObject = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Redraw(Attack attack)
        {
            var printedAccuracy = attack is null ? "-" :
                attack.Accuracy == Attack.CannotMiss ? "∞" : attack.Accuracy.ToString();
            var printedPower = (attack is null) || (attack.Target == StatusAttackTarget.User) ? "-" :
                attack.Power == Attack.OHKO ? "∞" : attack.Power.ToString();
            _tmObject.text = attack is null
                ? "Type -\n-/-"
                : $"Type {TypeToString.Convert(attack.Type)}\n{printedPower} / {printedAccuracy}";
        }
    }
}