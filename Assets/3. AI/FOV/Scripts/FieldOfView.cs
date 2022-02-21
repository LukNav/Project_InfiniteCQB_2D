using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldOfView : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("FOV Settings")]
    public float fov = 90f;
    public int rayCount = 20;
    public float viewDistance = 7f;
    public LayerMask obstacleLayers;
    public GameObject fovPrefab;//prefab with mesh filter and mesh renderer components
    public bool shouldVisualiseFOVField = false;
    public float fovObstacleOverlapDistance = 0.1f;

    [Header("Target tracking")]
    public LayerMask targetsLayers;

    [Header("Dependency Settings")]
    public bool useLocalTransform = true;//If false: use SetOrigin and SetAimDirection methods to update the transform values

    [Header("Debugging. Don't touch")]
    public List<Transform> visibleTargets = new List<Transform>();
    private int lastVisibleTargetsListCount = 0;

    Vector3 _origin;
    float _startingAngle = 0f;
    float _angle;
    float _angleIncrease;
    Mesh _mesh;
    float _dotAngleProduct;
    GameObject _fovGameObject;
    //public GameObject targetLockPrefab;
    //private GameObject _targetLockImage;

    void Start()
    {
        if(_mesh == null)
        {
            CreateFieldOfViewMesh();
        }

        _angleIncrease = fov / rayCount;
        _origin = Vector3.zero;

        _dotAngleProduct = Mathf.Abs(Vector3.Dot(transform.up, Utils.GetVectorFromAngle((360 - fov) / 2)));

        //_targetLockImage = Instantiate(targetLockPrefab);
    }

    private void CreateFieldOfViewMesh()
    {
        _mesh = new Mesh();
        _mesh.bounds = new Bounds(_origin, Vector3.one * 2000);
        _fovGameObject = Instantiate(fovPrefab);
        MeshFilter meshFilter = _fovGameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
            Debug.LogError("fovPrefab MUST have Mesh filter and Mesh renderer");
        meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        if(useLocalTransform)
        {
            SetOrigin(transform.position);
            SetAimDirection(transform.up); 
        }

        if(shouldVisualiseFOVField)
            VisualiseFOVCone();
        //if(lastVisibleTargetsListCount != visibleTargets.Count)
        SetVisibleTargets();

    }


    private void VisualiseFOVCone()
    {
        _angle = _startingAngle;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = _origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            Vector3 direction = Utils.GetVectorFromAngle(_angle);
            RaycastHit2D raycastHit = Physics2D.Raycast(_origin, direction, viewDistance, obstacleLayers);
            if (!raycastHit)
            {
                vertex = _origin + Utils.GetVectorFromAngle(_angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit.point + (raycastHit.point - new Vector2(_origin.x, _origin.y)) *fovObstacleOverlapDistance;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)//We run it only after the first loop, because we need origin vertex to connect to.
            {
                triangles[triangleIndex + 0] = vertexIndex;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = 0;
                triangleIndex += 3;
            }
            vertexIndex++;


            _angle -= _angleIncrease;//-= for clockwise rotation
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
    }

    private void SetVisibleTargets()
    {
        visibleTargets = new List<Transform>();

        Collider2D[] targets = Physics2D.OverlapCircleAll(_origin, viewDistance, targetsLayers);
        foreach (Collider2D target in targets)
        {
            Vector3 direction = target.transform.position - _origin;
            RaycastHit2D raycastHit = Physics2D.Raycast(_origin, direction, viewDistance, obstacleLayers);
            float distanceToTarget = Vector3.Distance(_origin, target.transform.position);

            if (!raycastHit || distanceToTarget < raycastHit.distance)
            {
                if (IsTargetInFOVField(target.transform.position))
                    visibleTargets.Add(target.transform);
            }
        }

        lastVisibleTargetsListCount = visibleTargets.Count;
    }

    private bool IsTargetInFOVField(Vector3 target)
    {
        Vector3 direction = target - _origin;
    
        float dotProduct = Vector3.Dot(transform.up, direction.normalized);// If confused what dot is - google Dot Product
        //Debug.Log("dotAngleProduct: " + _dotAngleProduct + " : DotProduct: " + dotProduct);
        bool isTargetInsideFOVCone = dotProduct > _dotAngleProduct;

        if (isTargetInsideFOVCone)
            return true;

        return false;
    }

    //private void HighlightVisibleTargets()
    //{
    //    if (visibleTargets.Count == 0)
    //        return;

    //    _targetLockImage.transform.position = visibleTargets[0].position;
    //}

    public void SetOrigin(Vector3 origin)
    {
        _origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        _startingAngle = Utils.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    /// <summary>
    /// Returns target's transform
    /// </summary>
    /// <returns>Transform of target. If there are no visible targets - returns null</returns>
    public Transform GetTarget()
    {
        if (visibleTargets.Count > 0)
            return visibleTargets[0];
        return null;
    }

    public void OnDisable()
    {
        if(_fovGameObject != null)
            _fovGameObject.SetActive(false);
    }

}
