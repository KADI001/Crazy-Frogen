using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ProgrammableJump))]
[RequireComponent(typeof(SideMovement))]
[RequireComponent(typeof(Gravity))]
public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float _runSpeed;

    private ProgrammableJump _jump;
    private SideMovement _turn;
    private Gravity _gravity;

    private Vector3 _delta;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _jump = GetComponent<ProgrammableJump>();
        _turn = GetComponent<SideMovement>();
        _gravity = GetComponent<Gravity>();
    }

    private void Start()
    {
        OnEnable();
    }

    private void FixedUpdate()
    {
        //_gravity.Fall();
        _turn.MoveToRoad();
        Run();

        _rigidbody.MovePosition(transform.position + _delta * Time.deltaTime);

        _delta = Vector3.zero;
    }

    private void OnEnable()
    {
        _jump.PositionChanged += p => _delta.y = p.y;
        _turn.PositionChanged += p => _delta.x = p.x;
        _gravity.PositionChanged += p => _delta.y += p.y;
    }

    private void OnDisable()
    {
        _jump.PositionChanged -= p => _delta.y = p.y;
        _turn.PositionChanged -= p => _delta.x = p.x;
        _gravity.PositionChanged -= p => _delta.y += p.y;
    }

    private void Run()
    {
        _delta.z += _runSpeed;
    }

    public void Jump()
    {
        _jump.JumpByTime();
    }

    public void Slide()
    {
        print("Slideeeeeeeeeee");
    }

    public void TurnLeft()
    {
        _turn.SwitchRoad(Vector3.left);
    }

    public void TurnRight()
    {
        _turn.SwitchRoad(Vector3.right);
    }
}