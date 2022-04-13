using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlideState))]
public class RunToSlideTransition : HeroTransition
{
    private void Awake()
    {
        NewState = GetComponent<SlideState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            NeedTransit = true;
        }
    }
}
