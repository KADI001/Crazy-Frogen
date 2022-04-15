using System;
using UnityEngine;

public class RunState : HeroState
{
    public event Action<Vector3> DeltaChanged;

    [SerializeField] private float _speed;

    private void Update()
    {
        
    }

    public void MoveForwardWithoutAcceleration()
    {
        float deltaZ = _speed * Time.deltaTime;

        DeltaChanged?.Invoke(new Vector3(0, 0, deltaZ));
    }
}