using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingHero : MonoBehaviour
{
    [SerializeField] private Transform _hero;
    [SerializeField] private float _speedOnZ;
    [SerializeField] private float _speedOnX;
    [SerializeField] private float _distanceFromHero;

    public Vector3 Target => new Vector3(_hero.position.x, transform.position.y, _hero.position.z - _distanceFromHero);

    private void FixedUpdate()
    {
        FollowHero();
    }

    private void FollowHero()
    {
        if (GetDistanceFromHero() < _distanceFromHero)
            return;

        Vector3 targetZ = new Vector3(transform.position.x, transform.position.y, Target.z);
        float lerpZ = Vector3.Lerp(transform.position, targetZ, _speedOnZ).z;

        Vector3 targetX = new Vector3(Target.x, transform.position.y, transform.position.z);
        float lerpX = Vector3.Lerp(transform.position, targetX, _speedOnX).x;

        Vector3 newPosition = new Vector3(lerpX, Target.y, lerpZ);

        transform.position = newPosition;
    }


    private float GetDistanceFromHero()
    {
        Vector3 cameraPosition = new Vector3(_hero.position.x, _hero.position.y, transform.position.z);

        return Vector3.Distance(cameraPosition, _hero.position);
    }
}
