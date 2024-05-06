using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationPanneau : MonoBehaviour
{
    public GameObject[] panneaux; // Tableau des panneaux à activer/désactiver
    private int panneauActifIndex = 0; // Indice du panneau actuellement affiché

    void Start()
    {
        // Désactive tous les panneaux sauf le premier au démarrage
        for (int i = 1; i < panneaux.Length; i++)
        {
            panneaux[i].SetActive(false);
        }
    }

    void Update()
    {
        // Navigation vers le panneau suivant avec le clic droit de la souris
        if (Input.GetMouseButtonDown(1)) // 1 correspond au bouton droit de la souris
        {
            if (panneauActifIndex == panneaux.Length - 1)
            {
                LancerOverworld();
            }
            else
            {
                AfficherPanneauSuivant();
            }
        }

        // Navigation vers le panneau précédent avec le clic gauche de la souris
        if (Input.GetMouseButtonDown(0)) // 0 correspond au bouton gauche de la souris
        {
            AfficherPanneauPrecedent();
        }
    }

    // Fonction pour afficher le panneau suivant
    void AfficherPanneauSuivant()
    {
        // Désactive le panneau actuel
        panneaux[panneauActifIndex].SetActive(false);

        // Passe au panneau suivant (ou reste au dernier panneau s'il est déjà affiché)
        panneauActifIndex = Mathf.Min(panneauActifIndex + 1, panneaux.Length - 1);

        // Active le nouveau panneau
        panneaux[panneauActifIndex].SetActive(true);
    }

    // Fonction pour afficher le panneau précédent
    void AfficherPanneauPrecedent()
    {
        // Désactive le panneau actuel
        panneaux[panneauActifIndex].SetActive(false);

        // Passe au panneau précédent (ou reste au premier panneau s'il est déjà affiché)
        panneauActifIndex = Mathf.Max(panneauActifIndex - 1, 0);

        // Active le nouveau panneau
        panneaux[panneauActifIndex].SetActive(true);
    }

    // Fonction pour lancer la scène Overworld
    void LancerOverworld()
    {
        SceneManager.LoadScene("Overworld");
    }
}
