using System;
using UnityEngine;

public class SideMovement : MonoBehaviour 
{
    public event Action<Vector3> PositionChanged;

    [SerializeField] [Min(0f)] private float _speed;

    private int _roadIndex;
    private int[] _roads = new int[3]
    {
        -2,
        0,
        2
    };

    private void Start()
    {
        _roadIndex = 1;
    }

    public int Road => _roads[_roadIndex];

    public void SwitchRoad(Vector3 direction)
    {
        if (direction.x > 0)
            _roadIndex++;
        else
            _roadIndex--;

        _roadIndex = Mathf.Clamp(_roadIndex, 0, _roads.Length - 1);
    }

    public void MoveToRoad()
    {
        Vector3 roadPosition = new Vector3(Road, transform.position.y, transform.position.z);
        Vector3 delta = Vector3.right * Vector3.MoveTowards(transform.position, roadPosition, _speed).x; 

        PositionChanged?.Invoke(delta);
    }
}
