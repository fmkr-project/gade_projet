using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 3f;

    private NavMeshAgent _agent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;
        var horizontalMove = Input.GetAxis("Horizontal");
        var verticalMove = Input.GetAxis("Vertical");

        var displacement = (Vector3.forward * verticalMove + Vector3.right * horizontalMove)
                           * (deltaTime * walkingSpeed);
        
    }
}
