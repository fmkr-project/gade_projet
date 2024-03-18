using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BagMenu : UpDownMenu
    {
        private Bag _bag;
        private int _maxPrintableItems;
        private List<GameObject> _textGameObjects;
        
        private new void Awake()
        {
            base.Awake();
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _bag = player.Bag;

            if (choices.Count <= 0)
                // Initialize item list
            {
                foreach (var pair in _bag.Contents)
                {
                    choices.Add($"{pair.Key.Name} (x{pair.Value})"); // TODO key items
                }
                choices.Add("QUITTER");
            }

            if (_textGameObjects.Count > 0)
                // Destroy existing text
            {
                foreach (var tObject in _textGameObjects)
                {
                    Destroy(tObject);
                }
            }
            
            // TODO pages
            // Print text in the menu
            var items = transform.Find("BagMenuItems");
            var textObject = items.Find("Text");
            //var layout = items.GetComponent<VerticalLayoutGroup>();
            foreach (var text in choices)
            {
                var newTextObject = Instantiate(textObject, items).gameObject;
                var newText = newTextObject.GetComponent<TextMeshProUGUI>();
                newText.text = text;
                newTextObject.SetActive(true);
            }
            
            

        }

        private void Redraw()
        {
            Awake();
        }
    }
}