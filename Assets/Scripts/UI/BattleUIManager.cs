using System.Collections;
using UnityEngine;

namespace UI
{
    public class BattleUIManager : MonoBehaviour
    {
        private BattleMessageBox _messageBox;
        private BattleActionBox _actionBox;
        private BattleDialogueBox _dialogueBox;

        private Coroutine _coroutine; // Save the coroutine to be able to stop it later
        
        void Awake()
        {
            _messageBox = transform.Find("Canvas/MessageBox").GetComponent<BattleMessageBox>();
            _actionBox = transform.Find("Canvas/ActionBox").GetComponent<BattleActionBox>();
            _dialogueBox = transform.Find("Canvas/DialogueBox").GetComponent<BattleDialogueBox>();
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

        public void NewDialogue(string text)
        {
            _coroutine = StartCoroutine(_dialogueBox.ProgressiveChangeBoxText(text));
        }

        public bool DialogueIsPrinting()
        {
            return _dialogueBox.isPrinting;
        }

        public void DialogueExpeditePrinting()
            // Instantly display the dialogue if it is still printing
        {
            StopCoroutine(_coroutine);
            _dialogueBox.isPrinting = false;
            _dialogueBox.InstantChangeBoxText();
        }

        public void DialogueClose()
        {
            _dialogueBox.Close();
        }
    }
}
