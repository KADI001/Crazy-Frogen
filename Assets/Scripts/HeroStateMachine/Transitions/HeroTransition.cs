using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroTransition : MonoBehaviour
{
    public HeroState NewState { get; protected set; }

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
