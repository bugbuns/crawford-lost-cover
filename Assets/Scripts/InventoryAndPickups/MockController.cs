using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MockController : MonoBehaviour
{
    public Rigidbody _rb;

    public PlayerStats _PlayerStats;
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _PlayerStats = new PlayerStats();
        _playerInput = GetComponent<PlayerInput>();

    }

    void Start()
    {
        _rb.velocity = new Vector3(3, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
