using UnityEngine;

public static class TransformExtension
{
    public static Vector3 MoveTowardsWithDelta(this Transform transform, Vector3 start, Vector3 end, float delta)
    {
        float distance = Vector3.Distance(start, end);
        Vector3 direction = (end - start).normalized;

        if (delta > distance)
            delta = distance;

        return direction * delta;
    }
} 
