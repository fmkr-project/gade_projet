using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleAudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;
    private AudioSource _lowHealthSource;
    private AudioSource _attackSource;
    private AudioSource _deathSource;
    
    private void Awake()
    {
        _bgmSource = transform.Find("MusicSource").GetComponent<AudioSource>();
        _lowHealthSource = transform.Find("LowHealthSource").GetComponent<AudioSource>();
        _attackSource = transform.Find("AttackSource").GetComponent<AudioSource>();
        _deathSource = transform.Find("DeathSource").GetComponent<AudioSource>();
    }

    public void LowHealthEffect()
    {
        _lowHealthSource.PlayOneShot(_lowHealthSource.clip);
    }

    public void AttackEffect()
    {
        _attackSource.PlayOneShot(_attackSource.clip);
    }

    public void DeathEffect()
    {
        _deathSource.PlayOneShot(_deathSource.clip);
    }
}
