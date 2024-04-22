using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public string nextSceneName = "NouvelleScene"; 
    public Light directionalLight;
    public bool isMovable = true;
    private Camera _camera;
    
    public bool isTransitioning;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

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
            GameInformation.SetPosition(transform.position);
            GameInformation.SetCameraPosition(_camera.transform.position);
            Debug.Log(GameInformation.PlayerPosition);
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