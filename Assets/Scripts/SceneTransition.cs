using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public string nextSceneName = "NouvelleScene"; 
    public Light directionalLight;
    public bool isMovable = true;

    public bool isTransitioning;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            isTransitioning = true;
            Debug.Log("yo");
        
            if (directionalLight == null)
            {
                directionalLight = GameObject.FindObjectOfType<Light>(); 
            }

            isMovable = false;
            StartCoroutine(FadeLightAndLoadScene());
        }
        
    }

    private IEnumerator FadeLightAndLoadScene()
    {
        float initialIntensity = directionalLight.intensity;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            
            float t = timer / fadeDuration;
            directionalLight.intensity = Mathf.Lerp(initialIntensity, 0f, t);

            timer += Time.deltaTime;
            yield return null;
        }

        
        SceneManager.LoadScene(nextSceneName);
    }
}