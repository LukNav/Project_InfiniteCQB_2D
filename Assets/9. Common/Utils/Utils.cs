using System.Collections;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetVectorFromAngle(float angle) //y >> Z(Forward); Z >> X(right) -- conversion from 3d coordinate system to 2d
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Sin(angleRad), Mathf.Cos(angleRad), 0f);
    }

    public static float GetAngleFromVectorFloat(Vector3 aimDirection)
    {
        float n = Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg;
        if (n < 0f)
            n += 360f;
        return n;
    }
}

/*#region Object Tracking implementation
    private void SetVisibleTargets()
    {
        visibleTargets = new List<Transform>();

        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, targetLayers);
        foreach(Collider target in targets)
        {
            Transform targetTransform = target.transform;
            if (IsTargetInFOVField(targetTransform.position))
                visibleTargets.Add(targetTransform);
        }
    }

    private bool IsTargetInFOVField(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        float angle = Utils.GetAngleFromVectorFloat(direction);
        angle = angle > 180 ? angle - 360 : angle;

        float rightLimit = Utils.GetAngleFromVectorFloat(transform.forward) + fov / 2f;
        float leftLimit = Utils.GetAngleFromVectorFloat(transform.forward) - fov / 2f;
        if (rightLimit >= angle
            && leftLimit <= angle)
            return true;

        return false;
    }

    private void HighlightVisibleTargets()
    {
        if (visibleTargets.Count == 0)
            return;

        _targetLockImage.transform.position = visibleTargets[0].position;
    }
    #endregion*/