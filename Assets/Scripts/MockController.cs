using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockController : MonoBehaviour
{
    public Rigidbody _rb;

    public PlayerStats _PlayerStats;
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _PlayerStats = new PlayerStats();
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
