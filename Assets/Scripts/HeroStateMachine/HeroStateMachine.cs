using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStateMachine : MonoBehaviour
{
    [SerializeField] private HeroState _firstState;

    private HeroState _currentState;

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        HeroState newState = _currentState.GetNextState();

        Transit(newState);
    }

    private void Transit(HeroState state)
    {
        if (state == null)
            return;

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
