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
        private List<GameObject> _textGameObjects = new();

        private Player _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _bag = _player.Bag;
        }
        
        public void Redraw()
        // Redraw when the player uses an item
        {
            if (choices.Count > 0) choices.Clear();
            // Initialize item list
            foreach (var pair in _bag.Contents)
            {
                choices.Add($"{pair.Key} (x{pair.Value})"); // TODO key items
            }
            choices.Add("QUITTER");

            // Destroy existing text
            if (_textGameObjects.Count > 0)
            {
                foreach (var tObject in _textGameObjects)
                {
                    Destroy(tObject);
                }

                _textGameObjects.Clear();
            }
            
            // TODO pages
            // Print text in the menu
            var items = transform.Find("BagMenuItems");
            var textObject = items.Find("Text");
            //var layout = items.GetComponent<VerticalLayoutGroup>();
            foreach (var text in choices)
            {
                var newTextObject = Instantiate(textObject, items).gameObject;
                _textGameObjects.Add(newTextObject);
                var newText = newTextObject.GetComponent<TextMeshProUGUI>();
                newText.text = text;
                newTextObject.SetActive(true);
            }
            
            

        }
    }
}