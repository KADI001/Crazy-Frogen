using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunState), typeof(BoxCollider))]
public class SlideToRunTransition : HeroTransition
{
    public const int DefaultHeroColliderHeight = 1;
    private BoxCollider _heroColider;

    private void Awake()
    {
        NewState = GetComponent<RunState>();
        _heroColider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        float currentHeroColliderHeight = _heroColider.size.y;

        if (currentHeroColliderHeight == DefaultHeroColliderHeight)
            NeedTransit = true;
    }
}
