using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LateralMenu : UpDownMenu
    {
        private new void Awake()
        {
            base.Awake();
            choices = new List<string> {"ÉQUIPE", "SAC", "QUITTER"};
            
            var items = transform.Find("LateralMenuItems");
            
            // Create TextMeshPro items
            var textObject = items.Find("Text");
            foreach (var item in choices)
            {
                var newTextObject = Instantiate(textObject, items).gameObject;
                var newText = newTextObject.GetComponent<TextMeshProUGUI>();
                newText.text = item;
                newTextObject.SetActive(true);
            }
        }

    }
}