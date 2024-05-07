using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 6f;
    private NavMeshAgent _agent;
    private SceneTransition _sceneTransition;
    private OverworldMenus _menus;
    public Animator _animator;
    public static bool change;
    
    
    // Start is called before the first frame update
    void Awake()
    { 
        _sceneTransition = GetComponent<SceneTransition>();
        _agent = GetComponent<NavMeshAgent>();
        _menus = FindObjectOfType<OverworldMenus>();
        //_animator = GetComponent<Animator>();


    }
    // Update is called once per frame
    void Update()
    {
        if (_sceneTransition.isMovable && _menus.Focus is null)
        {
           
            //Debug.Log(_agent.hasPath);
            var deltaTime = Time.deltaTime;
            var horizontalMove = Input.GetAxis("Horizontal");
            var verticalMove = Input.GetAxis("Vertical");

            var finalSpeed = Input.GetAxis("Sprint") != 0 ? sprintSpeed : walkingSpeed;
            _agent.speed = finalSpeed;

            var displacement = Vector3.forward * verticalMove + Vector3.right * horizontalMove;
            _agent.SetDestination(transform.position + displacement);

            // Vérifiez si le joueur se déplace et déclenchez l'animation
            if (_agent.velocity.magnitude > 0)
            {
                change = true;
                //_animator.SetBool("isMoving", true);
            }
            else
            {
                change = false;
                //_animator.SetBool("isMoving", false);
            }

        }
       
    }
}