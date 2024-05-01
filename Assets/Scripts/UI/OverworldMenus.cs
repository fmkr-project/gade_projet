using System;
using UnityEngine;

namespace UI
{
    public class OverworldMenus : MonoBehaviour
    {
        private LateralMenu _lateralMenu;
        private BagMenu _bagMenu;
        private TeamMenu _teamMenu;
        
        // Static menus
        private ObjectBox _objectMenu;
        private ObjectDesc _objectDesc;
        private YesNoBox _yesNoMenu;

        [NonSerialized] public UpDownMenu Focus = null;
        [NonSerialized] private UpDownMenu _previousFocus = null;
        
        // Start is called before the first frame update
        void Awake()
        {
            _lateralMenu = FindObjectOfType<LateralMenu>();
            _lateralMenu.CloseMenu();
            _bagMenu = FindObjectOfType<BagMenu>();
            _bagMenu.CloseMenu();
            _teamMenu = FindObjectOfType<TeamMenu>();
            _teamMenu.CloseMenu();
            
            // Static menus initialization
            _objectMenu = FindObjectOfType<ObjectBox>();
            _objectMenu.CloseMenu();
            _objectMenu.Redraw();
            _objectDesc = FindObjectOfType<ObjectDesc>();
            _objectDesc.Display(false);
            _yesNoMenu = FindObjectOfType<YesNoBox>();
            _yesNoMenu.CloseMenu();
            _yesNoMenu.Redraw();
        }

        // Update is called once per frame
        void Update()
        {
            // Manage static menus
            if (_objectMenu.open)
                // Object menu was invoked by the lateral menu i.e. can only use healing items
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow)) _objectMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _objectMenu.Navigate(1);
                
                // Use an item
                if (_objectMenu.GetChoice() == "UTILISER" && Input.GetKeyDown(KeyCode.Return))
                {
                    if (_bagMenu.GetSelectedItem() is not HealingItem
                        || !GameInformation.Bag.CanUseItem(_bagMenu.GetSelectedItem())) return;
                    // TODO inform the player (dialogue box)
                    _teamMenu.Mode = TeamMenuMode.Select;
                    _teamMenu.OpenMenu();
                    _teamMenu.Redraw();
                    _objectMenu.CloseMenu();
                    _previousFocus = _bagMenu;
                    Focus = _teamMenu;
                }
                
                // Toss an item
                if (_objectMenu.GetChoice() == "JETER" && Input.GetKeyDown(KeyCode.Return))
                {
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
                // Open bag menu
                if (_lateralMenu.GetChoice() == "SAC" && Input.GetKeyDown(KeyCode.Return))
                    // TODO enlever la variable magique
                {
                    _bagMenu.Redraw();
                    _bagMenu.OpenMenu();
                    _objectDesc.SetDescription(_bagMenu.GetSelectedItem());
                    Focus = _bagMenu;
                }
                
                // Open team menu
                if (_lateralMenu.GetChoice() == "ÉQUIPE" && Input.GetKeyDown(KeyCode.Return))
                {
                    _teamMenu.Mode = TeamMenuMode.View;
                    _teamMenu.OpenMenu();
                    _teamMenu.Redraw();
                    Focus = _teamMenu;
                }
                
                // Navigate in the lateral menu
                if (Input.GetKeyDown(KeyCode.UpArrow)) _lateralMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _lateralMenu.Navigate(1);
            }
            
            // Actions in the bag menu
            else if (Focus == _bagMenu)
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _bagMenu.Navigate(-1);
                    _objectDesc.SetDescription(_bagMenu.GetSelectedItem());
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _bagMenu.Navigate(1);
                    _objectDesc.SetDescription(_bagMenu.GetSelectedItem());
                }
                
                // Select object
                // TODO localize
                if (_bagMenu.GetChoice() != "QUITTER" && Input.GetKeyDown(KeyCode.Return))
                {
                    _objectMenu.OpenMenu();
                }
                
                // Exit
                if ((_bagMenu.GetChoice() == "QUITTER" && Input.GetKeyDown(KeyCode.Return))
                    || Input.GetKeyDown(KeyCode.Backspace))
                {
                    _bagMenu.CloseMenu();
                    Focus = _lateralMenu;
                }
            }
            else if (Focus == _teamMenu && _teamMenu.Mode == TeamMenuMode.View)
                // Team menu was invoked from the lateral menu
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow)) _teamMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _teamMenu.Navigate(1); // TODO unify this with Focus
                
                // Exit
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    _teamMenu.CloseMenu();
                    Focus = _lateralMenu;
                }
            }
            else if (Focus == _teamMenu && _teamMenu.Mode == TeamMenuMode.Select)
                // Team menu was invoked from the Bag menu
            // TODO other cases ?
            {
                // Navigate
                if (Input.GetKeyDown(KeyCode.UpArrow)) _teamMenu.Navigate(-1);
                if (Input.GetKeyDown(KeyCode.DownArrow)) _teamMenu.Navigate(1); // TODO unify this with Focus

                // Exit
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    _teamMenu.CloseMenu();
                    Focus = _previousFocus;
                    _previousFocus = null;
                }
                
                // Select a mon to heal
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    var item = (HealingItem) _bagMenu.GetSelectedItem();
                    var restored = item.HealedHp;
                    var newCreature = GameInformation.Squad.Monsters[_teamMenu.GetMonPosition()];
                    newCreature.CurrentHp = Math.Min(newCreature.MaxHp, newCreature.CurrentHp + restored);
                    GameInformation.Bag.TossItem(item);
                    _teamMenu.UpdateMonUnderCursor(newCreature);
                    _teamMenu.CloseMenu();
                    _bagMenu.Redraw();
                    Focus = _previousFocus;
                    _previousFocus = null;
                }
            }
            

            // Update screen
            _lateralMenu.gameObject.SetActive(_lateralMenu.open);
            _bagMenu.gameObject.SetActive(_bagMenu.open);
            _objectDesc.gameObject.SetActive(_bagMenu.open);
            _teamMenu.gameObject.SetActive(_teamMenu.open);
            _objectMenu.gameObject.SetActive(_objectMenu.open);
            _yesNoMenu.gameObject.SetActive(_yesNoMenu.open);
        }
    }
}
