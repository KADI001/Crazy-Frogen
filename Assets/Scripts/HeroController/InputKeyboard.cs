using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IMovement))]
public class InputKeyboard : MonoBehaviour
{
    private Dictionary<KeyCode, Action> _binds = new Dictionary<KeyCode, Action>();
    private IMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<IMovement>();
    }

    private void Start()
    {
        DefaultBinds();
    }

    private void Update()
    {
        foreach (var bind in _binds)
        {
            if (Input.GetKeyDown(bind.Key))
            {
                bind.Value?.Invoke();
            }
        }
    }

    public void DefaultBinds()
    {
        _binds[KeyCode.D] = _movement.TurnRight;
        _binds[KeyCode.A] = _movement.TurnLeft;
        _binds[KeyCode.Space] = _movement.Jump;
        _binds[KeyCode.S] = _movement.Slide;
    }

    public void BindButton(KeyCode keyCode, MovementCommand command)
    {
        switch (command)
        {
            case MovementCommand.TurnRight:
                BindButton(keyCode, _movement.TurnRight);
                break;
            case MovementCommand.TurnLeft:
                BindButton(keyCode, _movement.TurnLeft);
                break;
            case MovementCommand.Jump:
                BindButton(keyCode, _movement.Jump);
                break;
            case MovementCommand.Slide:
                BindButton(keyCode, _movement.Slide);
                break;
        }
    }

    private void BindButton(KeyCode keyCode, Action command)
    {
        _binds[keyCode] = command;
    }
}