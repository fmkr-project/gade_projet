using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform _target;

    private Vector3 _currentVelocity;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position += offset;
        transform.rotation = Quaternion.LookRotation(-offset);
    }

    protected void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            _target.position + offset,
            ref _currentVelocity,
            0.2f);
    }
}