using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class BattleUIManager : MonoBehaviour
    {
        private BattleMessageBox _messageBox;
        private BattleActionBox _actionBox;
        private BattleDialogueBox _dialogueBox;
        private BattleAttackBox _attackBox;

        private BattleAttackDescInfobox _descBox;
        private BattleAttackSpecInfobox _specBox;

        private PlayerBattleFrame _playerInfo;
        private EnemyBattleFrame _enemyInfo;

        private Fader _fader;
        private const float FadeTime = 0.6f;
        
        // Arrows
        private BattleActionBoxArrow _actionBoxArrow;
        private BattleAttackBoxArrow _attackBoxArrow;

        private Coroutine _coroutine; // Save the coroutine to be able to stop it later
        
        // Flags
        [NonSerialized] public bool AttackMenuIsOpen = false;
        
        void Awake()
        {
            _messageBox = transform.Find("Canvas/MessageBox").GetComponent<BattleMessageBox>();
            _actionBox = transform.Find("Canvas/ActionBox").GetComponent<BattleActionBox>();
            _dialogueBox = transform.Find("Canvas/DialogueBox").GetComponent<BattleDialogueBox>();
            _dialogueBox.gameObject.SetActive(true);
            _attackBox = transform.Find("Canvas/AttackBox").GetComponent<BattleAttackBox>();
            _attackBox.gameObject.SetActive(false);
            _specBox = transform.Find("Canvas/AttackInfoBox").GetComponent<BattleAttackSpecInfobox>();
            _specBox.gameObject.SetActive(false);
            _descBox = transform.Find("Canvas/AttackInfoBox2").GetComponent<BattleAttackDescInfobox>();
            _descBox.gameObject.SetActive(false);

            _playerInfo = GetComponentInChildren<PlayerBattleFrame>();
            _enemyInfo = GetComponentInChildren<EnemyBattleFrame>();

            _fader = transform.Find("Canvas/Fader").GetComponent<Fader>();
            _fader.gameObject.SetActive(true);
            StartCoroutine(_fader.FadeIn(FadeTime));
            
            _actionBoxArrow = transform.Find("Canvas/ActionBoxArrow").GetComponent<BattleActionBoxArrow>();
            _attackBoxArrow = transform.Find("Canvas/AttackBoxArrow").GetComponent<BattleAttackBoxArrow>();
            _attackBoxArrow.gameObject.SetActive(false);
            
            Debug.Log(GameInformation.PlayerPosition);
        }

        public void FetchMonsInfo(Creature player, Creature enemy)
        {
            _playerInfo.TrackedCreature = player;
            _enemyInfo.TrackedCreature = enemy;
        }

        public void InitializeMonsInfo()
        {
            _playerInfo.Initialize();
            _playerInfo.InitialRedraw();
            
            _enemyInfo.Initialize();
            _enemyInfo.InitialRedraw();
        }

        public void ReloadPlayerMonInfo()
        {
            _playerInfo.Redraw();
        }

        public void ReloadEnemyMonInfo()
        {
            _enemyInfo.Redraw();
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
        public void ActionOpenMenu()
        {
            _messageBox.gameObject.SetActive(true);
            _actionBox.gameObject.SetActive(true);
            _actionBoxArrow.gameObject.SetActive(true);
        }

        public void ActionCloseMenu()
        {
            _messageBox.gameObject.SetActive(false);
            _actionBox.gameObject.SetActive(false);
            _actionBoxArrow.gameObject.SetActive(false);
        }
        
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
        
        // Attack box
        public void AttackInitializeDraw(Creature playerMon)
        {
            _attackBox.Draw(playerMon);
        }

        public void AttackMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: _attackBoxArrow.Move(1);
                    break;
                case Direction.Up: _attackBoxArrow.Move(-1);
                    break;
                case Direction.Left:
                case Direction.Right:
                default:
                    return;
            }
        }

        public void AttackOpenMenu()
        {
            AttackMenuIsOpen = true;
            _attackBox.gameObject.SetActive(true);
            _specBox.gameObject.SetActive(true);
            _descBox.gameObject.SetActive(true);
            _attackBoxArrow.gameObject.SetActive(true);
        }
        
        public void AttackCloseMenu()
        {
            AttackMenuIsOpen = false;
            _attackBox.gameObject.SetActive(false);
            _specBox.gameObject.SetActive(false);
            _descBox.gameObject.SetActive(false);
            _attackBoxArrow.gameObject.SetActive(false);
        }

        public void AttackInfoRedraw(Creature playerMon)
            // Refresh attack information panels
        {
            var attack = AttackGetMonAttack(playerMon);
            _specBox.Redraw(attack);
            _descBox.Redraw(attack);
        }

        public string AttackGetChoice()
        {
            // Transform TMPro objects into strings
            var all = _attackBox.AttackTexts.Select(tmObject => tmObject.text).ToList();
            all.Add("RETOUR");

            return all[_attackBoxArrow.ArrowPosition];
        }

        public Attack AttackGetMonAttack(Creature usedMon)
        {
            return AttackGetChoice() != "-" && AttackGetChoice() != "RETOUR" ?
            usedMon.Attacks[_attackBoxArrow.ArrowPosition] : null;
        }

    }
}
