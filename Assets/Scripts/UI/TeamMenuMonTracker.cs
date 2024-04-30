using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TeamMenuMonTracker : MonoBehaviour
    {
        [NonSerialized] public Creature TrackedCreature;
        
        private TextMeshProUGUI _monNameLabel;
        private TextMeshProUGUI _monLevelLabel;
        private TextMeshProUGUI _monHealthLabel;
        private RectTransform _healthBarFg;

        private void Awake()
        {
            _monNameLabel = transform.Find("MonName").GetComponent<TextMeshProUGUI>();
            _monLevelLabel = transform.Find("MonLevel").GetComponent<TextMeshProUGUI>();
            _monHealthLabel = transform.Find("HealthText").GetComponent<TextMeshProUGUI>();
            _healthBarFg = transform.Find("HealthBarBg/HealthBarFg").GetComponent<RectTransform>();
        }

        public void Redraw()
        // Instant redraw
        {
            Awake(); // TODO NRE if awake is not called?
            if (TrackedCreature is null)
                throw new NullReferenceException("This tracker is not tracking a creature");
            _monNameLabel.text = TrackedCreature.Nickname;
            _monLevelLabel.text = $"N.{TrackedCreature.Level}";

            var curHp = TrackedCreature.CurrentHp;
            var maxHp = TrackedCreature.MaxHp;
            _monHealthLabel.text = $"{curHp} / {maxHp}";
            _healthBarFg.anchorMax = new Vector2((float) curHp / maxHp, 1);
        }
    }
}