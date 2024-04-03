using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class BattleSupervisor : MonoBehaviour
{
    private BattleUIManager _uiManager;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
