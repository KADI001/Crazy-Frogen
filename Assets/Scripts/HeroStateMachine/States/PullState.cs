using System;
using System.Collections;
using UnityEngine;

public class PullState : HeroState
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private int _force;
    [SerializeField] private LayerMask _collisionObjects;

    private Rigidbody _rigidbody;
    private Coroutine _pullCoroutine;

    private float _offset = 0.1f;

    public bool IsPulling => _pullCoroutine != null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        PullToGround();
    }

    private void PullToGround()
    {
        _pullCoroutine = StartCoroutine(ApplyStrongGravity());
    }

    public RaycastHit GetGroundRaycastHit()
    {
        Ray ray = new Ray(transform.position + Vector3.down * (transform.localScale.y * 0.5f - _offset), Vector3.down);
        RaycastHit hit = default;

        Physics.Raycast(ray, out hit, 20f, _collisionObjects);

        return hit;
    }

    private IEnumerator ApplyStrongGravity()
    {
        Vector3 gravityAcceleration = Physics.gravity * Time.deltaTime;
        float deltaY = 0;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;

        while (transform.position.y - transform.localScale.y * 0.5f != GetGroundRaycastHit().point.y)
        {
            yield return new WaitForFixedUpdate();

            Vector3 target = new Vector3(transform.position.x, GetGroundRaycastHit().point.y, transform.position.z);

            deltaY = transform.MoveTowardsWithDelta(transform.position + (Vector3.down * transform.localScale.y * 0.5f), target, _force * Time.deltaTime).y;

            DeltaChanged?.Invoke(new Vector3(0, deltaY, 0));
        }

        _rigidbody.useGravity = true;
        _pullCoroutine = null;
    }
}