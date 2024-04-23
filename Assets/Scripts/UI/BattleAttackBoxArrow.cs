using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class BattleAttackBoxArrow : MonoBehaviour
    {
        [NonSerialized] public int ArrowPosition;

        private Vector2 _initialArrowPosition; // const, but is initialized at runtime
        private static int _arrowDisplacement;
    
        // Start is called before the first frame update
        void Start()
        {
            _initialArrowPosition = transform.localPosition;
            _arrowDisplacement = (int) Resources.FindObjectsOfTypeAll<BattleAttackBox>()[0]
                .GetComponent<VerticalLayoutGroup>().spacing + 20; // TODO variable magique
        }

        public void Move(int y)
        {
            if (Math.Abs(y) > 1) return; // displacement must be 1
            ArrowPosition = Math.Clamp(ArrowPosition + y, 0, 4);
        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition = new Vector3(_initialArrowPosition.x,
                _initialArrowPosition.y - ArrowPosition * _arrowDisplacement);
        }
    }
}
