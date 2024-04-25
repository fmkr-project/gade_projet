using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [NonSerialized] public Creature TrackedCreature;

        private RectTransform _fg;
        private TextMeshProUGUI _hpText;

        private int _oldDisplayedHp;

        private void Awake()
        {
            _fg = transform.Find("Fg").GetComponent<RectTransform>();
            _hpText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }

        public void Initialize(Creature creature)
        {
            TrackedCreature = creature;
            _oldDisplayedHp = creature.CurrentHp;
        }

        public void Transition()
        {
            // TODO animation
            var displayedHp = Math.Max(TrackedCreature.CurrentHp, 0);
            _fg.anchorMax = new Vector2((float) displayedHp / TrackedCreature.MaxHp, 1);
            _hpText.text = $"{displayedHp} / {TrackedCreature.MaxHp}";
            _oldDisplayedHp = displayedHp;
        }
        
    }
}