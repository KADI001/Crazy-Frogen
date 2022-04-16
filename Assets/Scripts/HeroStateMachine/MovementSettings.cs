using UnityEngine;

[CreateAssetMenu(menuName = "Movement settings")]
public class MovementSettings : ScriptableObject
{
    [Header("Run")]
    [SerializeField] private float _runSpeed;

    [Header("Turn")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private int _stepWidth;

    [Header("Jump")]
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private AnimationCurve _trajectory;
    
    [Header("Gravity")]
    [SerializeField] private float _gravityScale;

    [Header("Slide")]
    [SerializeField] private float _slideDuration;
    [SerializeField] private float _heroHeight;

    public float RunSpeed => _runSpeed;
    public float TurnSpeed => _turnSpeed;
    public int StepWidth => _stepWidth;
    public float JumpDuration => _jumpDuration;
    public float JumpHeight => _jumpHeight;
    public AnimationCurve Trajectory => _trajectory;
    public float GravityScale => _gravityScale;
    public float SlideDuration => _slideDuration;
    public float HeroHeight => _heroHeight;
}
