using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityScale;
    [SerializeField] private AnimationCurve _trajectory;
    [SerializeField] private int _roadWidth;

    private Vector3 _delta;
    private Rigidbody _rigidbody;

    private int _roadLineIndex;
    private int[] _roadLines = new int[3] { -1, 0, 1 };

    public int RoadLineIndex
    {
        get => _roadLineIndex;
        private set
        {
            if (value >= _roadLines.Length || value < 0)
                return;

            _roadLineIndex = value;
        }
    }
    public int RoadLine => _roadLines[_roadLineIndex] * _roadWidth;
    public bool OnCurrentRoad => RoadLine == transform.position.x;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        RoadLineIndex = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            TurnRight();
        if (Input.GetKeyDown(KeyCode.A))
            TurnLeft();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(RoadLine, transform.position.y, transform.position.z);
        _delta.x = transform.MoveTowardsWithDelta(transform.position, newPosition, _turnSpeed * Time.deltaTime).x;
        _delta.z = (Vector3.forward * _runSpeed * Time.deltaTime).z;

        if (OnGround() == false)
            ApplyGravity(_gravityScale);

        _rigidbody.MovePosition(transform.position + _delta);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _delta.y = 0;
    }

    public void Jump()
    {
        StartCoroutine(MoveUpByTime());
    }

    public void ApplyGravity(float scale)
    {
        Vector3 acceleration = Physics.gravity * scale * Time.deltaTime;
        _delta.y += (acceleration * Time.deltaTime).y;
    }

    private bool OnGround()
    {
        Debug.DrawRay(transform.position + Vector3.down * transform.localScale.y * 0.5f, Vector3.down * 0.75f, Color.red);
        
        return Physics.Raycast(new Ray(transform.position + Vector3.down * transform.localScale.y * 0.5f, Vector3.down), out RaycastHit ray, 0.75f);
    }

    public void Slide()
    {
        print("Not working");
    }

    public void TurnLeft()
    {
        RoadLineIndex--;
    }

    public void TurnRight()
    {
        RoadLineIndex++;
    }

    private IEnumerator MoveUpByTime()
    {
        Vector3 startPosition = transform.position;
        float expiredTime = 0;
        float progress = 0;

        while (progress < 1)
        {
            yield return new WaitForFixedUpdate();

            expiredTime += Time.deltaTime;
            progress = expiredTime / _jumpDuration;

            Vector3 newPosition = new Vector3(transform.position.x, startPosition.y + _trajectory.Evaluate(progress) * _jumpHeight, transform.position.z);
            
            _delta.y += (newPosition - transform.position).y - _delta.y;
        }
    }
}