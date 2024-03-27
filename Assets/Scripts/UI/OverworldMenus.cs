using System;
using UnityEngine;

namespace UI
{
    public class OverworldMenus : MonoBehaviour
    {
        private LateralMenu _lateralMenu;
        private BagMenu _bagMenu;
        
        // Static menus
        private ObjectBox _objectMenu;
        private YesNoBox _yesNoMenu;

        [NonSerialized] public UpDownMenu Focus = null;
        
        // Start is called before the first frame update
        void Awake()
        {
            _lateralMenu = FindObjectOfType<LateralMenu>();
            _lateralMenu.CloseMenu();
            _bagMenu = FindObjectOfType<BagMenu>();
            _bagMenu.CloseMenu();
            
            // Static menus initialization
            _objectMenu = FindObjectOfType<ObjectBox>();
            _objectMenu.CloseMenu();
            _objectMenu.Redraw();
            _yesNoMenu = FindObjectOfType<YesNoBox>();
            _yesNoMenu.CloseMenu();
            _yesNoMenu.Redraw();
        }

        // Update is called once per frame
        void Update()
        {
            // Manage static menus
            if (_objectMenu.open)
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow)) _objectMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _objectMenu.Navigate(1);
                
                // Toss an item
                if (_objectMenu.GetChoice() == "JETER" && Input.GetKeyDown(KeyCode.Return))
                {
                    print(_bagMenu.GetSelectedItemName());
                    _bagMenu.Bag.TossItem(_bagMenu.GetSelectedItemName());
                    _bagMenu.Redraw();
                    _objectMenu.CloseMenu();
                }
                
                // Exit
                if (_objectMenu.GetChoice() == "RETOUR" && Input.GetKeyDown(KeyCode.Return))
                {
                    _objectMenu.CloseMenu();
                }
            }

            else if (_yesNoMenu.open)
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow)) _yesNoMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _yesNoMenu.Navigate(1);
                
                // Update choice
                _yesNoMenu.Result = _yesNoMenu.GetChoice() == "OUI";
                
                // Exit
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _yesNoMenu.CloseMenu();
                }
            }
            
            
            // Manage dynamic menus
            // Open / close lateral menu
            else if (Input.GetKeyDown(KeyCode.Space) && Focus is null)
            {
                _lateralMenu.OpenMenu();
                Focus = _lateralMenu;
            }

            else if (Input.GetKeyDown(KeyCode.Space) && Focus == _lateralMenu ||
                     Input.GetKeyDown(KeyCode.Return) && _lateralMenu.GetChoice() == "QUITTER")
            {
                _lateralMenu.CloseMenu();
                Focus = null;
            }
            
            // Actions in the lateral menu
            else if (Focus == _lateralMenu)
            {
                // Open / close bag menu
                if (_lateralMenu.GetChoice() == "SAC" && Input.GetKeyDown(KeyCode.Return))
                    // TODO enlever la variable magique
                {
                    _bagMenu.Redraw();
                    _bagMenu.OpenMenu();
                    Focus = _bagMenu;
                }
                
                // Navigate in the lateral menu
                if (Input.GetKeyDown(KeyCode.UpArrow)) _lateralMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _lateralMenu.Navigate(1);
            }
            
            // Actions in the bag menu
            else if (Focus == _bagMenu)
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow)) _bagMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _bagMenu.Navigate(1);
                
                // Select object
                // TODO localize
                if (_bagMenu.GetChoice() != "QUITTER" && Input.GetKeyDown(KeyCode.Return))
                {
                    _objectMenu.OpenMenu();
                }
                
                // Exit
                if (_bagMenu.GetChoice() == "QUITTER" && Input.GetKeyDown(KeyCode.Return))
                {
                    _bagMenu.CloseMenu();
                    Focus = _lateralMenu;
                }
            }

            // Update screen
            _lateralMenu.gameObject.SetActive(_lateralMenu.open);
            _bagMenu.gameObject.SetActive(_bagMenu.open);
            _objectMenu.gameObject.SetActive(_objectMenu.open);
            _yesNoMenu.gameObject.SetActive(_yesNoMenu.open);
        }
    }
}
