using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Try : MonoBehaviour
{
    private Animator _animator2;

    private void Start()
    {
        _animator2 = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerMover.change)
        {
            Debug.Log("Ca y est ca marche");
            _animator2.SetBool("isMoving", true);
        }
        if (!PlayerMover.change)
        {
            Debug.Log("Ca y est ca remarche");
            _animator2.SetBool("isMoving", false);
        }
}
}
