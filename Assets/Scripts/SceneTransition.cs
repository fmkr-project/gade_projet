using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.5f; 
    public string nextSceneName = "NouvelleScene"; 

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeToBlackAndLoadScene());
    }

    private IEnumerator FadeToBlackAndLoadScene()
    {
        
        GameObject canvasObject = new GameObject("FadingCanvas");
        CanvasGroup canvasGroup = canvasObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

       
        RectTransform rectTransform = canvasObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

      
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Charger la nouvelle scène
        SceneManager.LoadScene(nextSceneName);
    }
}