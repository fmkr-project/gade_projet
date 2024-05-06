using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [NonSerialized] public Creature TrackedCreature;

        private RectTransform _fg;
        private Image _fgImage;
        private TextMeshProUGUI _hpText;

        private int _oldDisplayedHp;
        private const float TransitionTime = 0.25f;

        private static readonly Color32 HealthOkColor = new (0, 168, 0, 255);
        private static readonly Color32 HealthLowColor = new (255, 214, 0,255);
        private static readonly Color32 HealthVeryLowColor = new (227, 33, 23,255);

        private void Awake()
        {
            _fg = transform.Find("Fg").GetComponent<RectTransform>();
            _fgImage = transform.Find("Fg").GetComponent<Image>();
            try
            {
                _hpText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            }
            catch (NullReferenceException e)
            {
                // Enemy bar has no text indicator
                _hpText = null;
            }
        }

        private void RepaintBar(int hp)
        {
            // Change bar color with HP
            var rate = (float) hp / TrackedCreature.MaxHp;
            switch (rate)
            {
                case > 0.5f: _fgImage.color = HealthOkColor;
                    break;
                case > 0.2f: _fgImage.color = HealthLowColor;
                    break;
                default: _fgImage.color = HealthVeryLowColor;
                    break;
            }
        }

        public void Initialize(Creature creature)
        {
            TrackedCreature = creature;
            _oldDisplayedHp = creature.CurrentHp;
        }

        public void InstantTransition()
        {
            // Transition, but for initialising box positions
            var displayedHp = TrackedCreature.CurrentHp;
            _fg.anchorMax = new Vector2((float) displayedHp / TrackedCreature.MaxHp, 1);
            if (_hpText is not null) _hpText.text = $"{displayedHp} / {TrackedCreature.MaxHp}";
            RepaintBar(displayedHp);
        }
        
        public IEnumerator Transition()
        {
            var displayedHp = Math.Max(TrackedCreature.CurrentHp, 0);
            var delta = _oldDisplayedHp - displayedHp;
            
            var elapsed = 0f;
            while (elapsed < TransitionTime)
            {
                var deltaTime = Time.deltaTime;
                var ratio = elapsed / TransitionTime;

                var anchorHp = _oldDisplayedHp - delta * ratio;
                var animHp = (int) Math.Round(anchorHp, 0);
                _fg.anchorMax = new Vector2(anchorHp / TrackedCreature.MaxHp, 1);
                if (_hpText is not null) _hpText.text = $"{animHp} / {TrackedCreature.MaxHp}";

                RepaintBar(animHp);

                yield return new WaitForSeconds(deltaTime);
                elapsed += deltaTime;
            }

            if (_hpText is not null) _hpText.text = $"{displayedHp} / {TrackedCreature.MaxHp}";
            _oldDisplayedHp = displayedHp;
        }
        
    }
}