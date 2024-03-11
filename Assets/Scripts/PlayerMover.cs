using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 20f;
    [SerializeField] private float sprintSpeed = 45f;

    private NavMeshAgent _agent;
    
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
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
