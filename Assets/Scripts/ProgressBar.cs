using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public AudioClip completionSound; // Son à jouer lorsque le progrès atteint 100%
    public AudioSource audioSource; // Source audio pour jouer le son

    void Start()
    {
        
    }

    // Méthode pour mettre à jour la valeur de la barre de progression
    public void SetProgress(float progress)
    {
        slider.value = progress;

        // Vérifier si le progrès a atteint 100%
        if (progress >= 1f)
        {
            // Jouer le son de complétion
            PlayCompletionSound();
        }
    }

    // Méthode pour jouer le son de complétion
    private void PlayCompletionSound()
    {
        // Vérifier si un son de complétion est défini
        if (completionSound != null && audioSource != null)
        {
            // Jouer le son de complétion
            audioSource.PlayOneShot(completionSound);
        }
        else
        {
            Debug.LogWarning("Le son de complétion n'est pas défini ou la source audio n'est pas attachée.");
        }
    }
}