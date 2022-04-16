using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PullState))]
public class JumpToPullTransition : HeroTransition
{
    private void Awake()
    {
        NewState = GetComponent<PullState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            NeedTransit = true;
    }
}
