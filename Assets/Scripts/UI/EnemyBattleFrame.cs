using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EnemyBattleFrame : MonoBehaviour
    {
        [NonSerialized] public Creature TrackedCreature;
        private TextMeshProUGUI _monName;
        private TextMeshProUGUI _monLevel;

        private HealthBar _enemyHpBar;
        
        private void Awake()
        {
            _monName = transform.Find("EnemyMonName").GetComponent<TextMeshProUGUI>();
            _monLevel = transform.Find("EnemyMonLevel").GetComponent<TextMeshProUGUI>();
            _enemyHpBar = GetComponentInChildren<HealthBar>();
        }
        
        public void Initialize()
        {
            if (TrackedCreature is null)
            {
                throw new ArgumentException("No enemy creature to track!");
            }
            
            _enemyHpBar.Initialize(TrackedCreature);

            _monName.text = TrackedCreature.Nickname;
            _monLevel.text = $"N.{TrackedCreature.Level.ToString()}";
        }
        
        public void Redraw()
        {
            StartCoroutine(_enemyHpBar.Transition());
        }

        public void InitialRedraw()
        {
            _enemyHpBar.InstantTransition();
        }
    }
}