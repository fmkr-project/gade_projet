using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    void Awake()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation, transform);
    }
}
