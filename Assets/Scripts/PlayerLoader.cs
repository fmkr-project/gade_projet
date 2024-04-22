using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Vector3 _newPosition;
    private Vector3 _newCameraPosition;
    private Camera _camera;
    
    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        Debug.Log(GameInformation.PlayerPosition);
        
            
        _newPosition = GameInformation.GetPosition();
        _newCameraPosition = GameInformation.GetCameraPosition();
        _camera.transform.position = _newCameraPosition;
        transform.position = _newPosition;
        Instantiate(playerPrefab, transform.position, transform.rotation, transform);
    
        
    }
}
