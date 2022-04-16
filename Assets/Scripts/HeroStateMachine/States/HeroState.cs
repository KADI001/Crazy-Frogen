using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroState : MonoBehaviour
{
    [SerializeField] protected HeroTransition[] _transitions;

    public void Enter() 
    {
        if(enabled == false)
        {
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
            }
        }    
    }

    public void Exit()
    {
        if (enabled == true)
        {
            enabled = false;

            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
        }
    }

    public HeroState GetNextState()
    {
        if(enabled == true)
        {
            foreach (var transition in _transitions)
            {
                if(transition.NeedTransit == true)
                {
                    return transition.NewState;
                }
            }
        }

        return null;
    }
}
