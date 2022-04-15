using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(JumpState))]
public class Gravity : MonoBehaviour
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private float _scale;
    [SerializeField] private LayerMask _collisionObjects;

    private JumpState _jump;
    private float _gravitySpeed;
    private Coroutine _gravityCoroutine;

    private void Update()
    {
        //if (OnGround() == false && _gravityCoroutine == null)
        //    _gravityCoroutine = StartCoroutine(ApplyGravity());
    }

    private void FixedUpdate()
    {

        if (GetGroundRaycastHit().distance != 0)
        {
            float deltaY = 0;
            Vector3 startPosition = transform.position + (Vector3.down * transform.localScale.y * 0.5f);
            Vector3 endPosition = GetGroundRaycastHit().point;
            _gravitySpeed -= _scale * Physics.gravity.y * Time.deltaTime * Time.deltaTime;
            deltaY = transform.MoveTowardsWithDelta(startPosition, endPosition, _gravitySpeed).y;

            DeltaChanged?.Invoke(new Vector3(0, deltaY, 0));
        }
        else
        {
            _gravitySpeed = 0;
        }
    }

    public bool OnGround()
    {
        Ray ray = new Ray(transform.position + Vector3.down * (transform.localScale.y * 0.5f - 0.01f), Vector3.down);

        return Physics.Raycast(ray, 0.01f, _collisionObjects);
    }

    public RaycastHit GetGroundRaycastHit()
    {
        Ray ray = new Ray(transform.position + Vector3.down * (transform.localScale.y * 0.5f - 0.1f), Vector3.down);
        RaycastHit hit = default;

        Physics.Raycast(ray, out hit, 20f, _collisionObjects);

        return hit;
    }

    private IEnumerator ApplyGravity()
    {
        Vector3 gravityAcceleration = Physics.gravity * Time.deltaTime;
        float deltaY = 0;
        float deltaGravitySpeed = 0;

        while (GetGroundRaycastHit().distance > 0.1f)
        {
            yield return new WaitForFixedUpdate();

            if(GetGroundRaycastHit().distance > 0.1f)
            {
                Vector3 target = new Vector3(transform.position.x, GetGroundRaycastHit().point.y, transform.position.z);

                deltaGravitySpeed += _scale * gravityAcceleration.y * Time.deltaTime;

                deltaY = transform.MoveTowardsWithDelta(transform.position, target, deltaGravitySpeed).y;

                DeltaChanged?.Invoke(new Vector3(0, deltaY, 0));
            } 
        }

        _gravityCoroutine = null;
    }
}