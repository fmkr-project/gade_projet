using System;
using UnityEngine;

namespace UI
{
    public class BattleActionBoxArrow : MonoBehaviour
    {
        private Vector2 _arrowPosition;

        private Vector2 _initialArrowPosition; // const, but is initialized at runtime
        private static Vector2 _arrowDisplacement = new (110, -35);
    
        // Start is called before the first frame update
        void Start()
        {
            _initialArrowPosition = transform.localPosition;
        }

        public void Move(int x, int y)
        {
            if (Math.Abs(x) > 1 | Math.Abs(y) > 1) return; // displacement must be 1

            var ap = _arrowPosition;
            _arrowPosition = new Vector2(Math.Clamp(ap.x + x, 0, 1), Math.Clamp(ap.y + y, 0, 1));
        }

        // Update is called once per frame
        void Update()
        {
            var screenWidth = Screen.width;
            transform.localPosition = _initialArrowPosition + _arrowPosition * _arrowDisplacement;
        }

        public string ReturnChoice()
        {
            return
                _arrowPosition.y == 0 ? _arrowPosition.x == 0 ? "ATTAQUE" : "CHANGER"
                : _arrowPosition.x == 0 ? "SAC" : "FUITE";
        }
    }
}
