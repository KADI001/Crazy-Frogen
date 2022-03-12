using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ProgrammableJump))]
[RequireComponent(typeof(SideMovement))]
public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float _runSpeed;

    private ProgrammableJump _jump;
    private SideMovement _turn;

    private Vector3 _target;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _jump = GetComponent<ProgrammableJump>();
        _turn = GetComponent<SideMovement>();
    }

    private void Start()
    {

        _target = transform.position;
        OnEnable();
    }

    private void FixedUpdate()
    {
        Run();
        _turn.MoveToRoad();

        _rigidbody.MovePosition(_target);
    }

    private void OnEnable()
    {
        _jump.PositionChange += p => _target.y = p.y;
        _turn.PositionChange += p => _target.x = p.x;
    }

    private void OnDisable()
    {
        _jump.PositionChange -= p => _target.y = p.y;
        _turn.PositionChange -= p => _target.x = p.x;
    }

    private void Run()
    {
        _target.z += _runSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        StartCoroutine(_jump.MoveByTime());
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