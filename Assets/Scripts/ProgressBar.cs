using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider _slider; 
    

    // Méthode pour mettre à jour la valeur de la barre de progression
    public void SetProgress(float progress)
    {
        _slider.value = progress;
    }
}