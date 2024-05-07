using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    // Nom de la scène à charger après le délai
    public string sceneName = "MainScreen";

    public GameObject player;
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

        GameInformation.Squad = new Squad();
        GameInformation.Bag = new Bag();
        GameInformation.SetPosition(new Vector3(50.0f, 0.15f, 46.0f));

        // Charger la scène MainMenu
        SceneManager.LoadScene(sceneName);
    }
}