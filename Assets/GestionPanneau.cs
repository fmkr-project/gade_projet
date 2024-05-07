using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationPanneau : MonoBehaviour
{
    public GameObject[] panneaux; 
    private int panneauActifIndex = 0; 

    void Start()
    {
       
        for (int i = 1; i < panneaux.Length; i++)
        {
            panneaux[i].SetActive(false);
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return)) // 1 correspond au bouton droit de la souris
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

        
        if (Input.GetMouseButtonDown(0)) 
        {
            AfficherPanneauPrecedent();
        }
    }

    // Fonction pour afficher le panneau suivant
    void AfficherPanneauSuivant()
    {
       
        panneaux[panneauActifIndex].SetActive(false);

        
        panneauActifIndex = Mathf.Min(panneauActifIndex + 1, panneaux.Length - 1);

        
        panneaux[panneauActifIndex].SetActive(true);
    }

    
    void AfficherPanneauPrecedent()
    {
      
        panneaux[panneauActifIndex].SetActive(false);

       
        panneauActifIndex = Mathf.Max(panneauActifIndex - 1, 0);

      
        panneaux[panneauActifIndex].SetActive(true);
    }

    
    void LancerOverworld()
    {
        SceneManager.LoadScene("Overworld");
    }
}
