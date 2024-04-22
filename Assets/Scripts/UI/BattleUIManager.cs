using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class BattleUIManager : MonoBehaviour
    {
        private BattleMessageBox _messageBox;
        private BattleActionBox _actionBox;
        private BattleDialogueBox _dialogueBox;

        private Fader _fader;
        private const float FadeTime = 0.6f;
        
        // Arrow "in" the action box
        private BattleActionBoxArrow _actionBoxArrow;

        private Coroutine _coroutine; // Save the coroutine to be able to stop it later
        
        void Awake()
        {
            _messageBox = transform.Find("Canvas/MessageBox").GetComponent<BattleMessageBox>();
            _actionBox = transform.Find("Canvas/ActionBox").GetComponent<BattleActionBox>();
            _dialogueBox = transform.Find("Canvas/DialogueBox").GetComponent<BattleDialogueBox>();
            _dialogueBox.gameObject.SetActive(true);

            _fader = transform.Find("Canvas/Fader").GetComponent<Fader>();
            _fader.gameObject.SetActive(true);
            StartCoroutine(_fader.FadeIn(FadeTime));
            
            _actionBoxArrow = transform.Find("Canvas/ActionBoxArrow").GetComponent<BattleActionBoxArrow>();
            
            Debug.Log(GameInformation.PlayerPosition);
        }

        // Update is called once per frame
        void Update()
        {
            // TODO lock inputs
        }

        public void LoadPlayerMonPrompt(Creature playerMon)
        {
            _messageBox.ChangeBoxText($"Que doit faire<br>{playerMon.Nickname} ?");
        }

        // Dialogue box
        public void NewDialogue(string text)
        {
            _coroutine = StartCoroutine(_dialogueBox.ProgressiveChangeBoxText(text));
        }

        public bool HasDialogueOnScreen()
        {
            return _dialogueBox.gameObject.activeSelf;
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
        
        // Action box
        public void ActionMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: _actionBoxArrow.Move(0, 1);
                    break;
                case Direction.Up: _actionBoxArrow.Move(0, -1);
                    break;
                case Direction.Left: _actionBoxArrow.Move(-1, 0);
                    break;
                case Direction.Right: _actionBoxArrow.Move(1, 0);
                    break;
                default: throw new ArgumentException("Invalid value for argument direction.");
            }
        }

        public string ActionGetChoice()
        {
            return _actionBoxArrow.ReturnChoice();
        }

        public void ActionFlee()
        {
            StartCoroutine(_fader.FadeOut(FadeTime));
            
            SceneManager.LoadScene("Overworld");
        }
    }
}
