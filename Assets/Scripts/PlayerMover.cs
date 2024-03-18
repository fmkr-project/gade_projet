using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 6f;
    private NavMeshAgent _agent;
    private SceneTransition _sceneTransition;

    private OverworldMenus _menus;
    
    
    // Start is called before the first frame update
    void Awake()
    { 
        _sceneTransition = GetComponent<SceneTransition>();
        _agent = GetComponent<NavMeshAgent>();
        _menus = FindObjectOfType<OverworldMenus>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_sceneTransition.isMovable && _menus.Focus is null)
        {
            var deltaTime = Time.deltaTime;
            var horizontalMove = Input.GetAxis("Horizontal");
            var verticalMove = Input.GetAxis("Vertical");

            var finalSpeed = Input.GetAxis("Sprint") != 0 ? sprintSpeed : walkingSpeed;
            _agent.speed = finalSpeed;

            var displacement = Vector3.forward * verticalMove + Vector3.right * horizontalMove;
            _agent.SetDestination(transform.position + displacement);


        }
            
        
    }
    
    
}
