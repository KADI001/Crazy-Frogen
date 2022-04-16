using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class JumpState : HeroState
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private float _force;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.AddForce(Vector3.up * _force * 100, ForceMode.Force);
    }

    private void Update()
    {
       
    }
}