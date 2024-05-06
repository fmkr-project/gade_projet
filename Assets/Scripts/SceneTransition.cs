using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public string nextSceneName = "NouvelleScene"; 
    public Light directionalLight;
    public bool isMovable = true;
    private Camera _camera;

    private OverworldAudioManager _audioManager;
    
    public bool isTransitioning;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _audioManager = FindObjectOfType<OverworldAudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            isTransitioning = true;
            Debug.Log("yo");

            // Flash effect
            var fader = Resources.FindObjectsOfTypeAll<Fader>()[0];
            StartCoroutine(fader.Flash(0.1f, 0.1f));
        
            if (directionalLight == null)
            {
                directionalLight = GameObject.FindObjectOfType<Light>(); 
            }

            isMovable = false;
            GameInformation.SetPosition(transform.position);
            GameInformation.SetCameraPosition(_camera.transform.position);

            _audioManager.StartBattle();
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