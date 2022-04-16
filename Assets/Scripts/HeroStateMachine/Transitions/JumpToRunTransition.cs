using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunState))]
public class JumpToRunTransition : HeroTransition
{
    private void Awake()
    {
        NewState = GetComponent<RunState>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        NeedTransit = true;
    }
}
