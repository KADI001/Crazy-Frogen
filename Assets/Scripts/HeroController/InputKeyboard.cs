using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour
{
    private Dictionary<KeyCode, Action> _binds = new Dictionary<KeyCode, Action>();

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

    public void BindButton(KeyCode keyCode, Action command)
    {
        _binds.Remove(keyCode);
        _binds[keyCode] = command;
    }
}