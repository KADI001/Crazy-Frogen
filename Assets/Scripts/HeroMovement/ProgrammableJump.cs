using System;
using System.Collections;
using UnityEngine;

public class ProgrammableJump : MonoBehaviour
{
    public event Action<Vector3> PositionChanged;

    [SerializeField] private float _height;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _trajectory;

    private Coroutine _jump;

    public bool IsJumping => _jump != null;

    public void JumpByTime()
    {
        _jump = StartCoroutine(MoveByTime());
    }

    private IEnumerator MoveByTime()
    {
        Vector3 startPosition = transform.position;
        float expiredTime = 0;
        float progress = 0;

        while(progress < 1)
        {
            yield return new WaitForFixedUpdate();

            expiredTime += Time.deltaTime;
            progress = expiredTime / _duration;

            PositionChanged?.Invoke(Vector3.up * _trajectory.Evaluate(progress) * _height);
        }

        _jump = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsJumping)
            StopCoroutine(_jump);
    }
}