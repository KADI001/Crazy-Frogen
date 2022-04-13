using System;
using System.Collections;
using UnityEngine;

public class JumpState : HeroState
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private float _duration;
    [SerializeField] private float _height;
    [SerializeField] private AnimationCurve _trajectory;
    [SerializeField] private float _gravityScale;
    [SerializeField] private LayerMask _collisionObjects;

    private float _deltaY;
    public float DeltaY 
    {
        get => _deltaY;
        private set
        {
            _deltaY = value;
            DeltaChanged?.Invoke(new Vector3(0, _deltaY, 0));
        }
    }

    private void OnEnable()
    {
        StartCoroutine(MoveUpWithTrajectory());
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (OnGround() == false)
            ApplyGravity();
    }

    public bool OnGround()
    {
        Ray ray = new Ray(transform.position + Vector3.down * (transform.localScale.y * 0.5f - 0.01f), Vector3.down);

        return Physics.Raycast(ray, 0.01f, _collisionObjects);
    }

    private void ApplyGravity()
    {
        Vector3 acceleration = Physics.gravity * _gravityScale * Time.deltaTime;
        DeltaY += (acceleration * Time.deltaTime).y;
    }

    private IEnumerator MoveUpWithTrajectory()
    {
        Vector3 startPosition = transform.position;
        float expiredTime = 0;
        float progress = 0;

        while (progress < 1)
        {
            yield return new WaitForFixedUpdate();

            expiredTime += Time.deltaTime;
            progress = expiredTime / _duration;

            Vector3 newPosition = new Vector3(transform.position.x, startPosition.y + _trajectory.Evaluate(progress) * _height, transform.position.z);
            DeltaY = (newPosition - transform.position).y;
        }
    }
}