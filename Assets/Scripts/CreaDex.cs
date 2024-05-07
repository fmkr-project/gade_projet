using System.Collections.Generic;
using UnityEngine;

public class Creadex : MonoBehaviour
{
    private List<int> encounteredCreatureIds = new List<int>();
    public ProgressBar _progressBar;

    private void Start()
    {
       
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
       
        float progress = (float)encounteredCreatureIds.Count / 19f; 

        // Mettre à jour la barre de progression en utilisant le script ProgressBar
        if (_progressBar != null)
        {
            _progressBar.SetProgress(progress);
        }
    }

    
}