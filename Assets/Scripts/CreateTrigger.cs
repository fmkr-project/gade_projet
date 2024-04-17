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
            
            Transform otherTransform = player.GetComponent<Transform>();
            Vector3 currentPosition = otherTransform.position;
            int randomx = Random.Range(-5, 5);
            int randomz = Random.Range(-5, 5);
            if (randomx != 0 && randomz != 0)
            {
                spawnPosition = new Vector3(currentPosition.x + randomx, currentPosition.y,
                    currentPosition.z + randomz);
            }
            GameObject clone = Instantiate(prefab, spawnPosition, spawnpoint.rotation);
            yield return new WaitForSeconds(0.2f);
            Destroy(clone, 1.0f);
            
        }
       
    }
}
