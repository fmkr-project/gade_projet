using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class LateralMenu : MonoBehaviour
    {
        public bool open;

        private Image _image;

        public void ToggleMenu()
        {
            open = !open;
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            // Set renderer to not active
            _image.enabled = open;
        }
    }
}