using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BagMenu : UpDownMenu
    {
        public Bag Bag;
        private int _maxPrintableItems;
        private List<GameObject> _textGameObjects = new();

        private Player _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Bag = GameInformation.Bag;
        }
        
        public void Redraw()
        // Redraw when the player uses an item
        {
            if (choices.Count > 0) choices.Clear();
            // Initialize item list
            foreach (var pair in Bag.PrintedContents)
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
            
            // TODO pages (cf TeamMenu)
            // Print text in the menu
            // An item is not suppressed from the list if its quantity is = 0 (easier)
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

        public string GetSelectedItemName()
        {
            var choice = GetChoice();
            
            // Remove item amount
            var splitted = choice.Split(' ').ToList();
            splitted.RemoveAt(splitted.Count-1);
            return String.Join(" ", splitted);
        }

        public Item GetSelectedItem()
        {
            try
            {
                return Bag.ItemLink[GetSelectedItemName()];
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
        }
    }
}