using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] public string sceneName; 

    // Méthode appelée lors du clic sur le bouton
    public void OnClick()
    {
        // Charger la scène par son nom
        SceneManager.LoadScene(sceneName);
    }
}
