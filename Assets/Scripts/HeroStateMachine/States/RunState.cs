using System;
using UnityEngine;

public class RunState : HeroState
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private float _speed;

    private float _deltaZ;

    public float DeltaZ
    {
        get => _deltaZ;
        private set
        {
            _deltaZ = value;
            DeltaChanged?.Invoke(new Vector3(0, 0, _deltaZ));
        }
    }

    private void Update()
    {
        
    }

    public void MoveForwardWithoutAcceleration()
    {
        DeltaZ = _speed * Time.deltaTime;
    }
}