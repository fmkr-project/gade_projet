using UnityEngine;

namespace UI
{
    public class BattleUIManager : MonoBehaviour
    {
        private BattleMessageBox _messageBox;
        private BattleActionBox _actionBox;
        
        void Awake()
        {
            _messageBox = transform.Find("Canvas/MessageBox").GetComponent<BattleMessageBox>();
            _actionBox = transform.Find("Canvas/ActionBox").GetComponent<BattleActionBox>();
        }

        // Update is called once per frame
        void Update()
        {
            // TODO lock inputs
            // Inputs to move the arrow in the action menu
        }

        public void LoadPlayerMonPrompt(Creature playerMon)
        {
            _messageBox.ChangeBoxText($"Que doit faire<br>{playerMon.Nickname} ?");
        }
    }
}
