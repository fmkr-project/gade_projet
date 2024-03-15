using UnityEngine;

namespace UI
{
    public class OverworldMenus : MonoBehaviour
    {
        private LateralMenu _lateralMenu;
        
        // Start is called before the first frame update
        void Awake()
        {
            _lateralMenu = FindObjectOfType<LateralMenu>();
        }

        // Update is called once per frame
        void Update()
        {
            // Open / close lateral menu
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _lateralMenu.ToggleMenu();
            }
            
            // Update screen
            _lateralMenu.gameObject.SetActive(_lateralMenu.open);
        }
    }
}
