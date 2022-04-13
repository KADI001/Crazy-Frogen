using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SlideState : HeroState
{
    [SerializeField] private float _duration;
    [SerializeField] private float _heroHeight;

    private BoxCollider _collider;
    private Coroutine _slideCoroutine;
    public readonly float DefaultHeroColliderHeight = 1;

    public bool isSliding => _slideCoroutine != null;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        Execute();
    }

    private void Execute()
    {
        if (_slideCoroutine != null)
            return;

        _slideCoroutine = StartCoroutine(LieDown());
    }

    private IEnumerator LieDown()
    {
        float newSizeY = _collider.size.y / transform.localScale.y * _heroHeight;
        float newCenterPositionY = _collider.center.y - _collider.size.y * 0.5f + newSizeY * 0.5f;
        _collider.size = new Vector3(1, newSizeY, 1);
        _collider.center = new Vector3(0, newCenterPositionY, 0);

        yield return new WaitForSeconds(_duration);

        GetUp();

        _slideCoroutine = null;
    }

    public void GetUp()
    {
        _collider.center = Vector3.zero;
        _collider.size = new Vector3(1, DefaultHeroColliderHeight, 1);
    }
}