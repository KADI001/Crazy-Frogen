using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpState))]
public class RunToJumpTransition : HeroTransition
{
    private void Awake()
    {
        NewState = GetComponent<JumpState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NeedTransit = true;
        }
    }
}
