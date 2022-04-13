using System;
using System.Collections;
using UnityEngine;

public class TurnState : HeroState
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private float _speed;
    [SerializeField] private int _stepWidth;

    private Coroutine _executeCoroutine;
    private int _roadLineIndex;
    private int[] _roadLines = new int[3] { -1, 0, 1 };
    private float _deltaX;

    public float DeltaX 
    {
        get => _deltaX;
        private set
        {
            _deltaX = value;
            DeltaChanged?.Invoke(new Vector3(_deltaX, 0, 0));
        }
    }
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

    public int RoadLine => _roadLines[_roadLineIndex] * _stepWidth;
    public bool OnCurrentRoad => RoadLine == transform.position.x;

    private void Update()
    {
        
    }

    public void Execute(SwitchDirection direction)
    {
        if (_executeCoroutine != null)
        {
            StopCoroutine(_executeCoroutine);
            _executeCoroutine = null;
        }

        switch (direction)
        {
            case SwitchDirection.Right:
                {
                    RoadLineIndex++;
                }
                break;
            case SwitchDirection.Left:
                {
                    RoadLineIndex--;
                }
                break;
        }

        _executeCoroutine = StartCoroutine(SmoothMoveToRoad(_speed));
    }

    private IEnumerator SmoothMoveToRoad(float deltaPerSecond)
    {
        Vector3 target = new Vector3(RoadLine, transform.position.y, transform.position.z);

        while (transform.position != target)
        {
            yield return new WaitForFixedUpdate();

            DeltaX = transform.MoveTowardsWithDelta(transform.position, target, deltaPerSecond * Time.deltaTime).x;
        }

        _executeCoroutine = null;
    }

    public enum SwitchDirection
    {
        Right,
        Left
    }
}