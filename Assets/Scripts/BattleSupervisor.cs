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
    private int _index; // Index in the list of owned creatures
    
    // Graphics (non-UI)
    [NonSerialized] public GameObject PlayerObject;
    [NonSerialized] public GameObject EnemyObject;
    private GameObject _orbObject;

    private const float BonkDuration = 0.2f; // Attack animation duration
    private const float BonkDistance = 0.5f;
    private const float SpawnTime = 0.6f; // TODO unify this with BattleSceneLoader
    private const float TimeBetweenShakes = 0.8f;
    private const float ShakeDuration = 0.5f;
    private const float ShakeHeight = 0.4f;
    
    // Battle logic
    private bool _readyForBattle;
    private bool _menusLocked;
    private Attack _chosenAttack;
    private Attack _enemyChosenAttack;
    private bool _waitObjectAnimate;
    private bool _waitAttackResults;
    private bool _checkDie;
    private bool _isDespawning;
    private bool _battleWon;
    private bool _isCapturing;
    private bool _orbIsBouncing;
    private bool _monChangeForced; // previous mon died
    
    private float BoingFunction(float x)
    {
        // Function that describes the movement of the ball bouncing
        // roots: 0, 1; max: 1 @ 0.5
        return -4 * x * (x - 1);
    }
    
    void Awake()
    {
        _uiManager = transform.Find("/UIManager").GetComponent<BattleUIManager>();
        _index = GameInformation.GetBattleReadyCreatureIndex();
    }

    
    private void Start()
    {
        _uiManager.FetchMonsInfo(PlayerMon, EnemyMon);
        _uiManager.InitializeMonsInfo();
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
        // Priority : victory check > battle turn = dialogue > team (select)
        // > object usage > attack = bag = team (switch) > action

        if (_battleWon)
            // Battle logic stops
        {
            return;
        }
        
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
                    StartCoroutine(_uiManager.ActionFlee());
                    return;
                }
                _uiManager.DialogueClose();
                return;
            }
        }
        
        
        // Can't use menus during a battle turn
        if (_menusLocked || _isCapturing) return; 
        
        // Interact with the team menu (select target mon mode)
        // TODO unify with BattleSwitch mode
        if (_uiManager.TeamMenuIsOpen() && _uiManager.TeamGetMode() == TeamMenuMode.BattleItem)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _uiManager.TeamMoveUp();
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _uiManager.TeamMoveDown();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _uiManager.TeamCloseMenu();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                var selectedMon = _uiManager.TeamGetSelectedMon();
                _index = _uiManager.TeamGetCursorPosition();

                var item = _uiManager.BagGetSelectedItem();
                switch (item)
                    // is the same since the bag can't be interacted with
                {
                    case HealingItem potion:
                        if (selectedMon.CurrentHp == selectedMon.MaxHp) return;
                        selectedMon.Heal(potion);
                        StartCoroutine(UseHealingItem(selectedMon, potion));
                        _uiManager.TeamRedraw();
                        break;
                }

                _uiManager.ObjectCloseMenu();
                _uiManager.BagCloseMenu();
                _uiManager.TeamCloseMenu();

                StartCoroutine(BattleTurnWithoutPlayer());
                return;
            }
        }
        
        // Interact with the object menu
        if (_uiManager.ObjectMenuIsOpen())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _uiManager.ObjectMoveUp();
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _uiManager.ObjectMoveDown();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _uiManager.ObjectCloseMenu();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                var item = _uiManager.BagGetSelectedItem();
                switch (_uiManager.ObjectGetChoice())
                {
                    case "RETOUR":
                        _uiManager.ObjectCloseMenu();
                        break;
                    case "JETER":
                        GameInformation.Bag.TossItem(item);
                        _uiManager.BagRedraw();
                        _uiManager.ObjectCloseMenu();
                        break;
                    case "UTILISER":
                        GameInformation.Bag.TossItem(item);
                        _uiManager.BagRedraw();
                        
                        if (item is HealingItem)
                        {
                            _uiManager.TeamSetMode(TeamMenuMode.BattleItem);
                            _uiManager.TeamOpenMenu();
                            _uiManager.TeamRedraw();
                            break;
                        }

                        if (item is CaptureOrb orb)
                        {
                            _uiManager.ObjectCloseMenu();
                            _uiManager.BagCloseMenu();
                            _uiManager.ActionCloseMenu();
                            _isCapturing = true;
                            StartCoroutine(OrbAnimation(orb));
                            break;
                        }

                        return;
                    default:
                        Debug.LogWarning("should not happen");
                        break;
                }

                return;
            }
        }
        
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
        if (_uiManager.BagMenuIsOpen())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _uiManager.BagMoveUp();
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _uiManager.BagMoveDown();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (_uiManager.BagGetSelectedItem() is null)
                {
                    _uiManager.BagCloseMenu();
                    return;
                }

                _uiManager.ObjectOpenMenu();
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _uiManager.BagCloseMenu();
                return;
            }
        }
        
        // Interact with the team menu (switch mon mode)
        if (_uiManager.TeamMenuIsOpen() && _uiManager.TeamGetMode() == TeamMenuMode.BattleSwitch)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _uiManager.TeamMoveUp();
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _uiManager.TeamMoveDown();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _uiManager.TeamCloseMenu();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                var selectedMon = _uiManager.TeamGetSelectedMon();
                if (selectedMon == PlayerMon) return;
                _index = _uiManager.TeamGetCursorPosition();

                StartCoroutine(ChangeBattlingMon(selectedMon));
                
                _uiManager.TeamCloseMenu();
                
                return;
            }
        }
        
        // Interact with the action menu
        if (_uiManager.ActionMenuIsOpen())
        {
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
                    case "CHANGER":
                        _uiManager.TeamSetMode(TeamMenuMode.BattleSwitch);
                        _uiManager.TeamOpenMenu();
                        _uiManager.TeamRedraw();
                        break;
                    case "ATTAQUE":
                        _uiManager.AttackMenuIsOpen = true;
                        _uiManager.AttackOpenMenu();
                        _uiManager.AttackInfoRedraw(PlayerMon);
                        break;
                    case "SAC":
                        _uiManager.BagOpenMenu();
                        _uiManager.BagRedraw();
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

            return;
        }
    }

    
    // Battle logic functions

    private IEnumerator BattleTurnWithoutPlayer()
    {
        while (_uiManager.HasDialogueOnScreen() || _isCapturing)
            yield return new WaitForSeconds(Time.deltaTime);
        
        // Refresh displays
        _uiManager.ReloadPlayerMonInfo();
        _uiManager.ReloadEnemyMonInfo();
        
        // The player does not use an attack : switch / item use
        _uiManager.ActionCloseMenu();
        _menusLocked = true;
        
        // The enemy chooses its next attack (at random)
        _enemyChosenAttack = EnemyMon.Attacks[Random.Range(0, EnemyMon.Attacks.Count)];
        
        // TODO case where the player uses a Ball
        
        EnemyAttackAnimation();
        while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);

        EnemyAttackResults();
        while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);
            
        StartCoroutine(CheckPlayerDie());
        while (_checkDie) yield return new WaitForSeconds(Time.deltaTime);
        
        EndBattleTurn();
        yield return null;
    }
    
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
            
            StartCoroutine(CheckEnemyDie());
            while (_checkDie) yield return new WaitForSeconds(Time.deltaTime);
            
            if (_battleWon) yield break;
            
            EnemyAttackAnimation();
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);

            EnemyAttackResults();
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);
            
            StartCoroutine(CheckPlayerDie());
            while (_checkDie) yield return new WaitForSeconds(Time.deltaTime);
        }
        else
        {
            EnemyAttackAnimation();
            // Wait until the animations have finished
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);
            
            EnemyAttackResults();
            // Wait until the attack dialogue is closed
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);

            StartCoroutine(CheckPlayerDie());
            while (_checkDie) yield return new WaitForSeconds(Time.deltaTime);

            if (_monChangeForced) // end turn early if previous mon died
            {
                EndBattleTurn();
                yield break;
            }

            PlayerAttackAnimation();
            while (_waitObjectAnimate) yield return new WaitForSeconds(Time.deltaTime);

            PlayerAttackResults();
            while (_waitAttackResults) yield return new WaitForSeconds(Time.deltaTime);
            
            StartCoroutine(CheckEnemyDie());
            while (_checkDie) yield return new WaitForSeconds(Time.deltaTime);
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
        _uiManager.ReloadEnemyMonInfo();
    }
    
    private void EnemyAttackResults()
    {
        _waitAttackResults = true;
        StartCoroutine(AttackResult(EnemyMon, PlayerMon, _enemyChosenAttack));
        _uiManager.ReloadPlayerMonInfo();
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
            PlayerObject.transform.position = new Vector3(initialPos.x, initialPos.y,
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

    private IEnumerator ChangeBattlingMon(Creature newMon)
    {
        _uiManager.NewDialogue($"{PlayerMon.Nickname} ! Reviens !");
        _uiManager.ActionCloseMenu();
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
                
        // Update current creature info & set PlayerObject to a new prefab
        _isDespawning = true;
        StartCoroutine(Despawn(PlayerObject));
        while (_isDespawning) yield return new WaitForSeconds(Time.deltaTime);
        
        var playerPrefab = CreaturePrefabLoader.GetPrefabFromId(newMon.Id);
        var newPlayerObject =
            (GameObject) Instantiate(playerPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation);
        newPlayerObject.transform.localScale = Vector3.zero;
        PlayerObject = newPlayerObject;
        PlayerMon = newMon;
        
        _uiManager.NewDialogue($"Go ! {PlayerMon.Nickname} !");
        // Status bar is updated to use the new active creature
        _uiManager.FetchMonsInfo(PlayerMon, EnemyMon);
        _uiManager.InitializeMonsInfo();
        _uiManager.LoadPlayerMonPrompt(PlayerMon);
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
        StartCoroutine(Spawn(PlayerObject));
                
        StartCoroutine(BattleTurnWithoutPlayer());
        _uiManager.ActionOpenMenu();
    }

    private IEnumerator UseHealingItem(Creature target, HealingItem potion)
    {
        _uiManager.NewDialogue($"{target.Nickname}\nrécupère des PV !");
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
    }

    private IEnumerator OrbAnimationBuffer()
    {
        yield return new WaitForSeconds(TimeBetweenShakes);
        _orbIsBouncing = false;
    }
    
    private IEnumerator OrbAnimation(CaptureOrb usedOrb)
    {
        _uiManager.NewDialogue($"Vous lancez une {usedOrb.Name} !");
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
        
        var orbPrefab = OrbPrefabLoader.GetOrbObject(usedOrb);
        _orbObject =
            Instantiate(orbPrefab, EnemyObject.transform.position, EnemyObject.transform.rotation);
        _orbObject.transform.localScale = Vector3.zero;
        StartCoroutine(Spawn(_orbObject));
        StartCoroutine(Despawn(EnemyObject));
        
        _orbIsBouncing = true;
        StartCoroutine(OrbAnimationBuffer());
        while (_orbIsBouncing) yield return new WaitForSeconds(Time.deltaTime);

        var captureStates = usedOrb.TryCapture(EnemyMon);
        // Animate shakes
        for (var i = 0; i < Math.Min(3, captureStates.Count); i++)
        {
            var continueAnimation = captureStates[i];

            // Wait some time between ball shakes
            _orbIsBouncing = true;
            StartCoroutine(OrbAnimationBuffer());
            while (_orbIsBouncing) yield return new WaitForSeconds(Time.deltaTime);
            
            if (!continueAnimation) // interrupt & fail capture
            {
                print("oof");
                StartCoroutine(Despawn(_orbObject));
                StartCoroutine(Spawn(EnemyObject));
                _uiManager.NewDialogue($"Oh non !\nLa capture a échoué !");
                while (_uiManager.HasDialogueOnScreen())
                    yield return new WaitForSeconds(Time.deltaTime);
                _isCapturing = false;
                StartCoroutine(BattleTurnWithoutPlayer());
                yield break;
            }
            // Shake animation
            var elapsed = 0f;
            var startPos = _orbObject.transform.position + Vector3.zero;
            while (elapsed < ShakeDuration)
            {
                var deltaTime = Time.deltaTime;
                _orbObject.transform.position =
                    new Vector3(startPos.x, ShakeHeight * BoingFunction(elapsed / ShakeDuration), startPos.z);
                
                elapsed += deltaTime;
                yield return new WaitForSeconds(deltaTime);
            }
            // Reset object height
            _orbObject.transform.position = new Vector3(startPos.x, 0, startPos.z);
        }
        
        // Capture success / update team info
        _uiManager.NewDialogue($"{EnemyMon.Nickname}\nest capturé !");
        PlayerMon.ResetStatusAlterations();
        EnemyMon.ResetStatusAlterations();
        GameInformation.Squad.StoreMonster(EnemyMon);
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
        
        // Fadeout / battle end
        StartCoroutine(_uiManager.ActionFlee());
        _isCapturing = false;
    }

    private IEnumerator CheckPlayerDie()
    {
        _checkDie = true;

        if (!PlayerMon.IsDead())
        {
            _checkDie = false;
            yield break;
        }
        // Begin death animation
        _uiManager.NewDialogue($"{PlayerMon.Nickname}\nest K.O. !");

        StartCoroutine(Despawn(PlayerObject));
        
        // Don't advance until the dialogue is closed
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);


        var nextMonInfo = GameInformation.Squad.GetBattleReadyCreature();
        if (nextMonInfo.Item1 == -1) // No mons left -> game over
        {
            StartCoroutine(_uiManager.ActionGameOver());
            yield break;
        }

        PlayerMon = nextMonInfo.Item2;
        
        _uiManager.NewDialogue($"Go ! {PlayerMon.Nickname} !");
        // Don't advance until the dialogue is closed
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);
        var playerPrefab = CreaturePrefabLoader.GetPrefabFromId(PlayerMon.Id);
        var newPlayerObject =
            (GameObject) Instantiate(playerPrefab, PlayerObject.transform.position, PlayerObject.transform.rotation);
        newPlayerObject.transform.localScale = Vector3.zero;
        PlayerObject = newPlayerObject;
        StartCoroutine(Spawn(PlayerObject));
        
        // Status bar is updated to use the new active creature
        _uiManager.FetchMonsInfo(PlayerMon, EnemyMon);
        _uiManager.InitializeMonsInfo();
        _uiManager.LoadPlayerMonPrompt(PlayerMon);

        _monChangeForced = true;
        _checkDie = false;
    }
    
    private IEnumerator CheckEnemyDie()
    {
        _checkDie = true;

        if (!EnemyMon.IsDead())
        {
            _checkDie = false;
            yield break;
        }
        // Begin death animation
        var determinant = new List<char> {'a', 'e', 'i', 'o', 'u'}.Contains(EnemyMon.Nickname[0])
            ? "L'"
            : "Le ";
        _uiManager.NewDialogue($"{determinant}{EnemyMon.Nickname} ennemi\nest K.O. !");

        StartCoroutine(Despawn(EnemyObject));
        
        // Don't advance until the dialogue is closed
        while (_uiManager.HasDialogueOnScreen())
            yield return new WaitForSeconds(Time.deltaTime);

        _checkDie = false;

        _uiManager.NewDialogue("Object event.");
        StartCoroutine(_uiManager.ActionFlee());
        _battleWon = true;
        
        // Reset mon status alterations
        PlayerMon.ResetStatusAlterations();
        EnemyMon.ResetStatusAlterations();
    }

    private IEnumerator Despawn(GameObject mon)
    {
        var curve = FindObjectOfType<BattleSceneLoader>().spawnCurve;
        var elapsed = SpawnTime;
        while (elapsed > 0)
        {
            var deltaTime = Time.deltaTime;
            var delta = curve.Evaluate(elapsed);
            mon.transform.localScale = new Vector3(delta, delta, delta);
            elapsed -= deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }

        _isDespawning = false;
    }

    private IEnumerator Spawn(GameObject mon)
    {
        var curve = FindObjectOfType<BattleSceneLoader>().spawnCurve;
        var elapsed = 0f;
        while (elapsed < SpawnTime)
        {
            var deltaTime = Time.deltaTime;
            var delta = curve.Evaluate(elapsed);
            mon.transform.localScale = new Vector3(delta, delta, delta);
            elapsed += deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }
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

        GameInformation.Squad.UpdateMonStatus(_index, PlayerMon);
        GameInformation.SetData(null);
    }
}
