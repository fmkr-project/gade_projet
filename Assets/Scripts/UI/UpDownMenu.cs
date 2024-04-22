using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpDownMenu : MonoBehaviour
    // Menus with an arrow
    {
        public bool open;
        
        public List<string> choices;
        protected GameObject Arrow;
        private RectTransform _arrowRt;
        protected int ArrowPosition;

        private Vector2 _initialArrowPosition; // const, but is initialized at runtime
        
        protected int Step;     // y displacement

        protected void Awake()
        {
            Arrow = transform.Find("Arrow").gameObject;
            _arrowRt = Arrow.GetComponent<RectTransform>();
            _initialArrowPosition = Arrow.GetComponent<RectTransform>().anchoredPosition;
            Step = (int) GetComponentInChildren<VerticalLayoutGroup>().spacing;
        }

        public string GetChoice()
        {
            return choices[ArrowPosition];
        }
        
        public void ToggleMenu()
        {
            open = !open;
        }

        public void OpenMenu()
        {
            open = true;
        }

        public void CloseMenu()
        {
            open = false;
        }

        public void Navigate(int direction)
        {
            if (direction == -1 & ArrowPosition <= 0) return;
            if (direction == 1 & ArrowPosition >= choices.Count-1) return;
            
            ArrowPosition += direction;
            _arrowRt.anchoredPosition = new Vector2(_initialArrowPosition.x,
                _initialArrowPosition.y - Step * ArrowPosition);
        }
    }
}