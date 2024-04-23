using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleSupervisor : MonoBehaviour
{
    private BattleUIManager _uiManager;

    private bool _willFlee;
    
    // Battle internals
    // Creatures are stored in memory
    [NonSerialized] public Creature PlayerMon; // TODO must update the player's team
    [NonSerialized] public Creature EnemyMon;
    
    // Graphics (non-UI)
    [NonSerialized] public GameObject PlayerObject;
    [NonSerialized] public GameObject EnemyObject;

    private const float BonkDuration = 0.2f; // Attack animation duration
    private const float BonkDistance = 0.5f;
    
    // Battle logic
    private bool _readyForBattle;
    private bool _menusLocked;
    private Attack _chosenAttack;
    private Attack _enemyChosenAttack;
    private bool _waitObjectAnimate;
    private bool _waitAttackResults;
    
    void Awake()
    {
        _uiManager = transform.Find("/UIManager").GetComponent<BattleUIManager>();
    }

    private void Start()
    {
        _uiManager.LoadPlayerMonPrompt(PlayerMon);
        _uiManager.NewDialogue($"Un {EnemyMon.Nickname} sauvage apparaît !\n ");
        
        // Initialize inactive windows
        _uiManager.AttackInitializeDraw(PlayerMon);
    }
    
    // Battle logic
    private bool FirstAttackerIsPlayer()
    {
        var playerSpeed = PlayerMon.Speed;
        var enemySpeed = EnemyMon.Speed;
        // TODO alterations

        return playerSpeed >= enemySpeed;
    }
    
    

    // Supervisor manages keypresses
    void Update()
    {
        // Priority : battle turn = dialogue > object usage > attack = bag = team > action

        if (_readyForBattle)
        {
            StartCoroutine(BattleTurn());
            _readyForBattle = false;
        }
        
        // Interact with the dialogue
        if (_uiManager.HasDialogueOnScreen())
        {
            if (Input.GetKeyDown(KeyCode.Return) & _uiManager.DialogueIsPrinting())
            {
                _uiManager.DialogueExpeditePrinting();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return) & !_uiManager.DialogueIsPrinting())
            {
                if (_willFlee)
                {
                    _willFlee = false;
                    _uiManager.ActionFlee();
                    // TODO destroy this scene
                    return;
                }
                _uiManager.DialogueClose();
                return;
            }
        }
        
        
        // Can't use menus during a battle turn
        if (_menusLocked) return; 
        
        // Interact with the object menu
        
        // Interact with the attack menu
        if (_uiManager.AttackMenuIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _uiManager.AttackMove(Direction.Up);
                _uiManager.AttackInfoRedraw(PlayerMon);
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _uiManager.AttackMove(Direction.Down);
                _uiManager.AttackInfoRedraw(PlayerMon);
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _uiManager.AttackCloseMenu();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (_uiManager.AttackGetChoice())
                {
                    // Not an attack
                    case "-": return;
                    // Exit attack menu
                    case "RETOUR":
                        _uiManager.AttackCloseMenu();
                        return;
                    // Is an attack
                    default:
                        _chosenAttack = _uiManager.AttackGetMonAttack(PlayerMon);
                        print(_uiManager.AttackGetChoice());
                        _uiManager.AttackCloseMenu();
                        _readyForBattle = true;
                        return;
                }
            }
        }
        
        // Interact with the bag menu
        
        // Interact with the team menu
        
        // Interact with the action menu
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _uiManager.ActionMove(Direction.Up);
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _uiManager.ActionMove(Direction.Down);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _uiManager.ActionMove(Direction.Left);
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _uiManager.ActionMove(Direction.Right);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (_uiManager.ActionGetChoice())
            {
                case "ATTAQUE":
                    _uiManager.AttackMenuIsOpen = true;
                    _uiManager.AttackOpenMenu();
                    _uiManager.AttackInfoRedraw(PlayerMon);
                    break;
                case "FUITE":
                    _willFlee = true;
                    _uiManager.NewDialogue("Vous prenez la fuite !\n ");
                    break;
                default:
                    throw new ArgumentException("This action is not yet implemented...");
            }

            return;
        }
    }

    
    // Battle logic functions
    
    private IEnumerator BattleTurn()
    {
        _uiManager.ActionCloseMenu();
        _menusLocked = true;
        
        // The enemy chooses its next attack (at random)
        _enemyChosenAttack = EnemyMon.Attacks[Random.Range(0, EnemyMon.Attacks.Count)];

        if (FirstAttackerIsPlayer())
        {
            PlayerAttackAnimation();
            // Wait until the animations have finished
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);

            PlayerAttackResults();
            // Wait until the attack dialogue is closed
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);
            
            EnemyAttackAnimation();
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);

            EnemyAttackResults();
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);
        }
        else
        {
            EnemyAttackAnimation();
            // Wait until the animations have finished
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);
            
            EnemyAttackResults();
            // Wait until the attack dialogue is closed
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);

            PlayerAttackAnimation();
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);

            PlayerAttackResults();
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);
        }
        
        EndBattleTurn();
        yield return null;
    }

    private void PlayerAttackAnimation()
    {
        _waitObjectAnimate = true;
        StartCoroutine(PlayerAttacks());
    }

    private void EnemyAttackAnimation()
    {
        _waitObjectAnimate = true;
        StartCoroutine(EnemyAttacks());
    }

    private void PlayerAttackResults()
    {
        _waitAttackResults = true;
        StartCoroutine(AttackResult(PlayerMon, EnemyMon, _chosenAttack));
    }
    
    private void EnemyAttackResults()
    {
        _waitAttackResults = true;
        StartCoroutine(AttackResult(EnemyMon, PlayerMon, _enemyChosenAttack));
    }

    private IEnumerator PlayerAttacks()
    {
        var initialPos = PlayerObject.transform.position + Vector3.zero; // deepcopy
        _uiManager.NewDialogue($"{PlayerMon.Nickname}\nutilise {_chosenAttack.Name} !");

        // Don't advance until the dialogue is closed
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);

        var elapsed = 0f;
        while (elapsed < BonkDuration / 2)
        {
            var deltaTime = Time.deltaTime;
            PlayerObject.transform.position = new(initialPos.x, initialPos.y,
                initialPos.z + (2 * elapsed / BonkDuration) * BonkDistance);
            elapsed += deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }

        elapsed = 0f;
        while (elapsed < BonkDuration / 2)
        {
            var deltaTime = Time.deltaTime;
            PlayerObject.transform.position = new(initialPos.x, initialPos.y,
                initialPos.z + BonkDistance - (2 * elapsed / BonkDuration) * BonkDistance);
            elapsed += deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }

        PlayerObject.transform.position = initialPos;
        _waitObjectAnimate = false;
    }
    
    private IEnumerator EnemyAttacks()
    {
        var initialPos = EnemyObject.transform.position + Vector3.zero; // deepcopy
        
        // le / l'
        var determinant = new List<char> {'a', 'e', 'i', 'o', 'u'}.Contains(EnemyMon.Nickname[0])
            ? "L'"
            : "Le ";
        _uiManager.NewDialogue($"{determinant}{EnemyMon.Nickname} ennemi\nutilise {_enemyChosenAttack.Name} !");

        // Don't advance until the dialogue is closed
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
        
        var elapsed = 0f;
        while (elapsed < BonkDuration / 2)
        {
            var deltaTime = Time.deltaTime;
            EnemyObject.transform.position = new(initialPos.x, initialPos.y,
                initialPos.z - (2 * elapsed / BonkDuration) * BonkDistance);
            elapsed += deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }

        elapsed = 0f;
        while (elapsed < BonkDuration / 2)
        {
            var deltaTime = Time.deltaTime;
            EnemyObject.transform.position = new(initialPos.x, initialPos.y,
                initialPos.z - BonkDistance + (2 * elapsed / BonkDuration) * BonkDistance);
            elapsed += deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }

        EnemyObject.transform.position = initialPos;
        
        _waitObjectAnimate = false;
    }

    private IEnumerator AttackResult(Creature source, Creature target, Attack attack)
    // Compute attack effects & display
    {
        var attackHits = source.TestAttackHits(attack, target);
        if (attackHits)
        {
            var efficiency = target.ReceiveAttack(attack, source);
            if (Math.Abs(efficiency - 1f) < 0.0001f)
            {
                _waitAttackResults = false;
                yield return null;
            }

            switch (efficiency)
            {
                case > 1f:
                    _uiManager.NewDialogue("C'est super efficace !");
                    break;
                case < 1f:
                    _uiManager.NewDialogue("Ce n'est pas très efficace...");
                    break;
            }
        }
        else
        {
            if (source == PlayerMon)
            {
                var determinant = new List<char> {'a', 'e', 'i', 'o', 'u'}.Contains(EnemyMon.Nickname[0])
                    ? "L'"
                    : "Le ";
                _uiManager.NewDialogue($"{determinant}{EnemyMon.Nickname} ennemi\nesquive l'attaque !");
            }
            else
            {
                _uiManager.NewDialogue($"{PlayerMon.Nickname}\nesquive l'attaque !");
            }
        }
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);

        _waitAttackResults = false;
    }

    private void EndBattleTurn()
    {
        // Reset battle logic variables
        _chosenAttack = null;
        _enemyChosenAttack = null;

        // Restore player prompt
        _menusLocked = false;
        _uiManager.ActionOpenMenu();
    }
}
