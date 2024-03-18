using UnityEngine;

namespace UI
{
    public class OverworldMenus : MonoBehaviour
    {
        private LateralMenu _lateralMenu;
        private BagMenu _bagMenu;
        
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _lateralMenu.ToggleMenu();
            }
            
            // Actions in the lateral menu
            if (_lateralMenu.open)
            {
                // Open / close bag menu
                if (_lateralMenu.GetChoice() == "SAC" & Input.GetKeyDown(KeyCode.KeypadEnter))
                    // TODO enlever la variable magique
                {
                    _bagMenu.OpenMenu();
                }
                
                // Navigate in the lateral menu
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _lateralMenu.Navigate(-1);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _lateralMenu.Navigate(1);
                }
            }

            // Update screen
            _lateralMenu.gameObject.SetActive(_lateralMenu.open);
        }
    }
}
