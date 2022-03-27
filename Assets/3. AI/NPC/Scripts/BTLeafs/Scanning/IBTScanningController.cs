using UnityEngine;

public interface IBTScanningController
{
    float scanningRotationAngle { get; set; }

    float scanningRotationSpeed { get; }
    Quaternion rotation { get; set; }

    bool hasRotated { get; }
    
}