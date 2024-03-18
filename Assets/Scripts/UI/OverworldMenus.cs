using System;
using UnityEngine;

namespace UI
{
    public class OverworldMenus : MonoBehaviour
    {
        private LateralMenu _lateralMenu;
        private BagMenu _bagMenu;

        [NonSerialized] public UpDownMenu Focus = null;
        
        // Start is called before the first frame update
        void Awake()
        {
            _lateralMenu = FindObjectOfType<LateralMenu>();
            _lateralMenu.CloseMenu();
            _bagMenu = FindObjectOfType<BagMenu>();
            _bagMenu.CloseMenu();
        }

        // Update is called once per frame
        void Update()
        {
            // Open / close lateral menu
            if (Input.GetKeyDown(KeyCode.Space) && Focus is null)
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
        }
    }
}
