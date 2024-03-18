using UnityEngine;

namespace UI
{
    public class LateralMenu : MonoBehaviour
    {
        public bool open;

        public void ToggleMenu()
        {
            open = !open;
        }
    }
}