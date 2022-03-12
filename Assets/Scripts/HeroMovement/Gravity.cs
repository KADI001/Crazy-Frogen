using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public event Action<Vector3> PositionChanged;

    [SerializeField] private float _scale;
    [SerializeField] private bool _onGround;

    private void Start()
    {
        _onGround = true;
    }

    public void Fall()
    {
        if(_onGround == false)
        {
            Vector3 delta = (transform.position - (Physics.gravity * Time.deltaTime * _scale) * Time.deltaTime);
            PositionChanged?.Invoke(delta);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.GetContact(0).normal.y >= 0.95)
        {
            _onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _onGround = false;
    }
}