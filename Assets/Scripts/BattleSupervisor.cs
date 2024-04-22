using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class BattleSupervisor : MonoBehaviour
{
    private BattleUIManager _uiManager;

    private bool _willFlee;
    
    // Battle internals
    // Creatures are stored in memory
    public Creature PlayerMon; // TODO must update the player's team
    public Creature EnemyMon;
    
    
    
    void Awake()
    {
        _uiManager = transform.Find("/UIManager").GetComponent<BattleUIManager>();
    }

    private void Start()
    {
        _uiManager.LoadPlayerMonPrompt(PlayerMon);
        _uiManager.NewDialogue($"Un {EnemyMon.Nickname} sauvage apparaît !\n ");
    }

    // Supervisor manages keypresses
    void Update()
    {
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
}
