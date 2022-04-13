using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(RunState), typeof(TurnState))]
[RequireComponent(typeof(JumpState))]
public class Movement : MonoBehaviour
{
    [SerializeField] private InputKeyboard _inputKeyboard;

    private JumpState _jumping;
    private TurnState _turning;
    private RunState _runing;
    private Rigidbody _rigidbody;
    private Vector3 _delta;

    private void Awake()
    {
        _inputKeyboard = GetComponent<InputKeyboard>();
        _rigidbody = GetComponent<Rigidbody>();
        _jumping = GetComponent<JumpState>();
        _turning = GetComponent<TurnState>();
        _runing = GetComponent<RunState>();
    }

    private void Start()
    {
        _inputKeyboard.BindButton(KeyCode.D, TurnToRight);
        _inputKeyboard.BindButton(KeyCode.A, TurnToLeft);
    }

    private void FixedUpdate()
    {
        _runing.MoveForwardWithoutAcceleration();

        Move();
    }

    private void OnEnable()
    {
        _jumping.DeltaChanged += OnDeltaYChanged;
        _turning.DeltaChanged += OnDeltaXChanged;
        _runing.DeltaChanged += OnDeltaZChanged;
    }

    private void OnDisable()
    {   
        _jumping.DeltaChanged -= OnDeltaYChanged;
        _turning.DeltaChanged -= OnDeltaXChanged;
        _runing.DeltaChanged -= OnDeltaZChanged;
    }

    public void TurnToRight()
    {
        _turning.Execute(TurnState.SwitchDirection.Right);
    }

    public void TurnToLeft()
    {
        _turning.Execute(TurnState.SwitchDirection.Left);
    }

    private void OnDeltaYChanged(Vector3 delta)
    {
        _delta.y = delta.y;
    }

    private void OnDeltaXChanged(Vector3 delta)
    {
        _delta.x = delta.x;
    }

    private void OnDeltaZChanged(Vector3 delta)
    {
        _delta.z = delta.z;
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + _delta);

        _delta = Vector3.zero;
    }
}