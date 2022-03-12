using System;
using System.Collections;
using UnityEngine;

public class ProgrammableJump : MonoBehaviour
{
    public event Action<Vector3> PositionChange;

    [SerializeField] private float _height;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _trajectory;

    public IEnumerator MoveByTime()
    {
        Vector3 startPosition = transform.position;
        Vector3 newPosition = Vector3.zero;
        float expiredTime = 0;
        float progress = 0;

        while(progress < 1)
        {
            yield return new WaitForFixedUpdate();

            expiredTime += Time.deltaTime;
            progress = expiredTime / _duration;

            newPosition.y = startPosition.y + _trajectory.Evaluate(progress) * _height;

            PositionChange?.Invoke(newPosition);
        }
    }    
}