using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldAudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;
    private AudioSource _battleStartSource;
    
    private void Awake()
    {
        _bgmSource = transform.Find("OverworldSource").GetComponent<AudioSource>();
        _battleStartSource = transform.Find("BattleStartSource").GetComponent<AudioSource>();
    }

    public void StartBattle()
    {
        _bgmSource.Stop();
        _battleStartSource.PlayOneShot(_battleStartSource.clip);
    }
}
