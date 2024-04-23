using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class BattleAttackBox : MonoBehaviour
    {
        [NonSerialized] public List<TextMeshProUGUI> AttackTexts;
        
        private void Start()
        {
            AttackTexts = new List<TextMeshProUGUI>
            {
                transform.Find("Attack1Text").GetComponent<TextMeshProUGUI>(),
                transform.Find("Attack2Text").GetComponent<TextMeshProUGUI>(),
                transform.Find("Attack3Text").GetComponent<TextMeshProUGUI>(),
                transform.Find("Attack4Text").GetComponent<TextMeshProUGUI>()
            };
        }

        public void Draw(Creature playerMon)
        {
            Start();
            print(AttackTexts);
            var moveset = playerMon.Attacks;
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    AttackTexts[i].text = moveset[i].Name;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    AttackTexts[i].text = "-";
                }
            }
        }
    }
}