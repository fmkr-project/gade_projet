using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerBattleFrame : MonoBehaviour
    {
        [NonSerialized] public Creature TrackedCreature;
        private TextMeshProUGUI _monName;
        private TextMeshProUGUI _monLevel;

        private HealthBar _playerHpBar;
        

        private void Awake()
        {
            _monName = transform.Find("PlayerMonName").GetComponent<TextMeshProUGUI>();
            _monLevel = transform.Find("PlayerMonLevel").GetComponent<TextMeshProUGUI>();
            _playerHpBar = GetComponentInChildren<HealthBar>();
        }

        public void Initialize()
        {
            if (TrackedCreature is null)
            {
                throw new ArgumentException("No player creature to track!");
            }
            
            _playerHpBar.Initialize(TrackedCreature);

            _monName.text = TrackedCreature.Nickname;
            _monLevel.text = $"N.{TrackedCreature.Level.ToString()}";
        }

        public void Redraw()
        {
            StartCoroutine(_playerHpBar.Transition());
        }

        public void InitialRedraw()
        {
            _playerHpBar.InstantTransition();
        }
    }
}