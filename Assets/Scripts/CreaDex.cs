using System.Collections.Generic;
using UnityEngine;

public class Creadex : MonoBehaviour
{
    private List<int> encounteredCreatureIds = new List<int>();
    public ProgressBar _progressBar;

    private void Start()
    {
        // Obtenez la liste de créatures de la squad dans GameInformation et passez-la à CheckSquad
        CheckSquad(GameInformation.Squad.Monsters);
        
    }

    public void CheckSquad(List<Creature> squad)
    {
        foreach (var creature in squad)
        {
            if (!encounteredCreatureIds.Contains(creature.Id))
            {
                encounteredCreatureIds.Add(creature.Id);
                Debug.Log($"Nouvelle créature rencontrée : {creature.Id}");
            }
        }
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        // Calculer le pourcentage de créatures rencontrées par rapport au total
        float progress = (float)encounteredCreatureIds.Count / 18f; // 18 est le nombre total de créatures

        // Mettre à jour la barre de progression en utilisant le script ProgressBar
        if (_progressBar != null)
        {
            _progressBar.SetProgress(progress);
        }
    }


    // Autres méthodes de gestion du Pokédex
}