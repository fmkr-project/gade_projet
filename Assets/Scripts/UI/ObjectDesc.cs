using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ObjectDesc : MonoBehaviour
    {
        private TextMeshProUGUI _descText;

        private void Awake()
        {
            _descText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetDescription(Item item)
        {
            _descText.text = item is null ? "" : item.Description;
        }

        public void Display(bool display)
        {
            gameObject.SetActive(display);
        }
    }
}