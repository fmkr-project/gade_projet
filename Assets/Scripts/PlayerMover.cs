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
    private Animator _animator; // Ajoutez une variable pour l'Animator
    
    // Start is called before the first frame update
    void Awake()
    { 
        _sceneTransition = GetComponent<SceneTransition>();
        _agent = GetComponent<NavMeshAgent>();
        _menus = FindObjectOfType<OverworldMenus>();
        _animator = GetComponent<Animator>(); // Attribuer la référence à l'Animator
        
        
    }

    private void Start()
    {
        _animator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_sceneTransition.isMovable && _menus.Focus is null)
        {
            _animator.SetBool("isMoving", true);
            Debug.Log(_agent.velocity.magnitude);
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
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);// Assurez-vous que le trigger est réinitialisé lorsque le joueur ne se déplace pas
            }
        }
        else
        {
            _animator.Play("Idle");
        }
    }
}