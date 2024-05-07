using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    // Nom de la scène à charger après le délai
    public string sceneName = "MainScreen";
    
    // Durée du délai en secondes
    public float delay = 10f;

    private void Start()
    {
        
        // Lance la coroutine pour charger la scène après le délai
        StartCoroutine(LoadSceneAfterDelay());
    }

    // Coroutine pour charger la scène après le délai
    private IEnumerator LoadSceneAfterDelay()
    {
        // Attendre le délai spécifié
        yield return new WaitForSeconds(delay);

        // Charger la scène MainMenu
        SceneManager.LoadScene(sceneName);
    }
}