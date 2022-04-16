using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(RunState), typeof(TurnState))]
[RequireComponent(typeof(JumpState), typeof(PullState))]
public class Movement : MonoBehaviour
{
    [SerializeField] private InputKeyboard _inputKeyboard;

    private PullState _pulling;
    private JumpState _jumping;
    private TurnState _turning;
    private RunState _runing;
    private Rigidbody _rigidbody;
    private Vector3 _delta;

    private void Awake()
    {
        _inputKeyboard = GetComponent<InputKeyboard>();
        _pulling = GetComponent<PullState>();
        _jumping = GetComponent<JumpState>();
        _turning = GetComponent<TurnState>();
        _runing = GetComponent<RunState>();
        _rigidbody = GetComponent<Rigidbody>();
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
        _pulling.DeltaChanged += OnDeltaYChanged;
        _jumping.DeltaChanged += OnDeltaYChanged;
        _turning.DeltaChanged += OnDeltaXChanged;
        _runing.DeltaChanged += OnDeltaZChanged;
    }

    private void OnDisable()
    {
        _pulling.DeltaChanged -= OnDeltaYChanged;
        _jumping.DeltaChanged -= OnDeltaYChanged;
        _turning.DeltaChanged -= OnDeltaXChanged;
        _runing.DeltaChanged -= OnDeltaZChanged;
    }

    public void TurnToRight()
    {
        _turning.TurnTo(TurnState.SwitchDirection.Right);
    }

    public void TurnToLeft()
    {
        _turning.TurnTo(TurnState.SwitchDirection.Left);
    }

    private void OnDeltaYChanged(Vector3 delta)
    {
        _delta.y += delta.y;
    }

    private void OnDeltaXChanged(Vector3 delta)
    {
        _delta.x += delta.x;
    }

    private void OnDeltaZChanged(Vector3 delta)
    {
        _delta.z += delta.z;
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + _delta);

        _delta = Vector3.zero;
    }
}
