using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneLoader : MonoBehaviour
{
    public GameObject creaturePrefab;
    public Transform playerBase;
    public Transform enemyBase;

    private void Start()
    {
        Instantiate(creaturePrefab, enemyBase.position,  Quaternion.identity);
        SceneManager.sceneLoaded += OnSceneLoaded;
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