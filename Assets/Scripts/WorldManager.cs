using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private SceneTransition _transitionManager;
    private Zone _currentZone;
        

    private bool _isSpawning;
    
    void Awake()
    {
        // Fade on overworld transition
        var fader = Resources.FindObjectsOfTypeAll<Fader>()[0];
        fader.gameObject.SetActive(true);
        StartCoroutine(fader.FadeIn(1, 0.5f));
        
        _transitionManager = transform.Find("/Player").GetComponent<SceneTransition>();
        _currentZone = GetComponentInChildren<Zone>();
        
        //print(GameInformation.squad);
    }

    // Update is called once per frame
    void Update()
    {
        if (_transitionManager.isTransitioning & !_isSpawning)
            // Prepare data of the new creature
        {
            _isSpawning = true;
            var newCreatureId = _currentZone.GetSpawnedCreatureById("Grass");
            // TODO use other spawn types : Cavern, TallGrass...
            // TODO level ranges
            var newCreature = new ConcreteCreatureFactory().GenerateCreature(newCreatureId, 10);
            GameInformation.SetData(newCreature);
            //A supprimer
            GameInformation.squad.StoreMonster(newCreature);
            
            //foreach (var kvp in GameInformation.squad.Monsters)
            //{
            //    Debug.Log("Clé : " + kvp.Key + ", Valeur : " + kvp.Value);
            //}
        }
        
        
        
    }
}
