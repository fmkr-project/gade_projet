using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 6f;

    private NavMeshAgent _agent;
    
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        var verticalMove = Input.GetAxis("Vertical");

        var finalSpeed = Input.GetAxis("Sprint") != 0 ? sprintSpeed : walkingSpeed;
        _agent.speed = finalSpeed;

        var displacement = Vector3.forward * verticalMove + Vector3.right * horizontalMove;
        _agent.SetDestination(transform.position + displacement);
    }
}
