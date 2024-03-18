using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 20f;
    [SerializeField] private float sprintSpeed = 45f;
    private NavMeshAgent _agent;
    private SceneTransition _sceneTransition;
    
    
    // Start is called before the first frame update
    void Awake()
    {
         _sceneTransition = GetComponent<SceneTransition>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_sceneTransition.isMovable == true)
        {
            var deltaTime = Time.deltaTime;
            var horizontalMove = Input.GetAxis("Horizontal");
            var verticalMove = Input.GetAxis("Vertical");

            var finalSpeed = Input.GetAxis("Sprint") != 0 ? sprintSpeed : walkingSpeed;

            var displacement = (Vector3.forward * verticalMove + Vector3.right * horizontalMove)
                               * (deltaTime * finalSpeed);
            _agent.SetDestination(transform.position + displacement);


        }
            
        
    }
    
    
}
