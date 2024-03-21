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

    private void Start()
    {
        var prefab = new CreaturePrefabLoader().GetPrefabFromId(6);
        
        var spawnTime = 0.6f; // temp, TODO do a cleaner version of this
        _enemyObject = (GameObject) Instantiate(prefab, enemyBase.position,
            Quaternion.Euler(0, 180, 0));
        
        // Initial scale is 0
        _enemyObject.transform.localScale = Vector3.zero;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        // Play a short animation
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        var time = spawnCurve[spawnCurve.keys.Length - 1].time;
        
        var remaining = 0f;
        while (remaining < time)
        {
            var deltaTime = Time.deltaTime;
            remaining += deltaTime;
            var delta = spawnCurve.Evaluate(remaining);
            _enemyObject.transform.localScale = new Vector3(delta, delta, delta);
            yield return new WaitForSeconds(deltaTime);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "BattleScene")
        {
            
           
        }
    }

    private void OnDestroy()
    {
       
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}