using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private SceneTransition _transitionManager;
    private Zone _currentZone;
        

    private bool _isSpawning;
    
    void Awake()
    {
        _transitionManager = transform.Find("/Player").GetComponent<SceneTransition>();
        _currentZone = GetComponentInChildren<Zone>();
        
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
        }
    }
}
