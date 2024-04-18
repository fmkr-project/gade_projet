using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Vector3 newPosition;
    private Vector3 newCameraPosition;
    public Camera camera;
    
    void Awake()
    {
        Debug.Log(GameInformation.playerPosition);
        
            
            newPosition = GameInformation.GetPosition();
            newCameraPosition = GameInformation.GetCameraPosition();
            camera.transform.position = newCameraPosition;
            transform.position = newPosition;
            Instantiate(playerPrefab, transform.position, transform.rotation, transform);
        
        
    }
}
