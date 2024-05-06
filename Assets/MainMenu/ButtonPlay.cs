using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoutonPlay : MonoBehaviour
{
    // Référence au bouton
    public Button bouton;
    public string scene;

    void Start()
    {
        // Ajoutez un écouteur d'événements pour le clic sur le bouton
        bouton.onClick.AddListener(JouerScene);
    }

    void JouerScene()
    {
        // Charger la scène "Overworld"
        SceneManager.LoadScene(scene);
    }
}
