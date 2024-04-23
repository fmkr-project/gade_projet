using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneLoader : MonoBehaviour
{
    public GameObject creaturePrefab;
    public Transform playerBase;
    public Transform enemyBase;

    [SerializeField] private AnimationCurve spawnCurve;

    private GameObject _enemyObject;
    private GameObject _playerObject;
    
    private void Start()
    {
        // Transfer the info to the supervisor
        var supervisor = transform.Find("/BattleSupervisor").GetComponent<BattleSupervisor>();
        
        // TODO Use player's team instead of a random fella
        supervisor.PlayerMon = new ConcreteCreatureFactory().GenerateCreature(1, 10);

        try
        {
            supervisor.EnemyMon = GameInformation.GetData();
        }
        catch (NullReferenceException e)
        {
            print("WARNING: enemy mon data not found, defaulting to placeholder mon");
            supervisor.EnemyMon = new ConcreteCreatureFactory().GenerateCreature(1, 10);
        }
        
        // Graphics
        var prefabLoader = new CreaturePrefabLoader();
        var playerPrefab = prefabLoader.GetPrefabFromId(supervisor.PlayerMon.Id);
        var enemyPrefab = prefabLoader.GetPrefabFromId(supervisor.EnemyMon.Id);
        var spawnTime = 0.6f; // temp, TODO do a cleaner version of this
        _enemyObject = (GameObject) Instantiate(enemyPrefab, enemyBase.position,
            Quaternion.Euler(0, 180, 0));
        _playerObject = (GameObject) Instantiate(playerPrefab, playerBase.position,
            Quaternion.Euler(Vector3.zero));

        // Initial scale is 0
        _enemyObject.transform.localScale = Vector3.zero;
        _playerObject.transform.localScale = Vector3.zero;
        
        // Play a short animation
        StartCoroutine(Spawn(_enemyObject));
        StartCoroutine(Spawn(_playerObject));
        
        // Save to the supervisor
        supervisor.PlayerObject = _playerObject;
        supervisor.EnemyObject = _enemyObject;
    }

    public IEnumerator Spawn(GameObject mon)
    {
        var time = spawnCurve[spawnCurve.keys.Length - 1].time;
        
        var remaining = 0f;
        while (remaining < time)
        {
            var deltaTime = Time.deltaTime;
            remaining += deltaTime;
            var delta = spawnCurve.Evaluate(remaining);
            mon.transform.localScale = new Vector3(delta, delta, delta);
            yield return new WaitForSeconds(deltaTime);
        }
    }

   

    
}