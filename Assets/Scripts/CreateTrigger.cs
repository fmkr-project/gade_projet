using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateTrigger : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnpoint;
    public GameObject player;
    private Vector3 spawnPosition;

    private void Start()
    {
        StartCoroutine("SpawnTriggers");
    }

   

    IEnumerator SpawnTriggers()
    {
        while (true)
        {
            var currentPosition = player.transform.position;
            var randomx = Random.Range(-5, 5);
            var randomz = Random.Range(-5, 5);
            
            // Don't spawn too close to the player
            if (Vector2.SqrMagnitude(new Vector2(randomx, randomz)) >= 4)
            {
                spawnPosition = new Vector3(currentPosition.x + randomx, currentPosition.y,
                    currentPosition.z + randomz);
            }
            else
            {
                continue;
            }
            var clone = Instantiate(prefab, spawnPosition, spawnpoint.rotation);
            yield return new WaitForSeconds(0.2f);
            Destroy(clone, 1.0f);
            
        }
       
    }
}
