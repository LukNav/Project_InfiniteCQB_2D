using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 mouseTarget { get; private set; }
    public float distanceFromCharacterToMouse { get; private set; }

    private CameraController _cameraController { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ShootingController shootingController = GetComponent<ShootingController>();
        if(shootingController == null)
        {
            Debug.LogError("Component not found: ShootingController");
        }
        PlayerInputController.input_OnFireDelegate += shootingController.Fire;

        StatsController statsController = GetComponent<StatsController>();
        if (statsController == null)
        {
            Debug.LogError("Component not found: StatsController");
        }
        statsController.deathDelegate += OnDeath;

        _cameraController = GetComponent<CameraController>();
        if (_cameraController == null)
        {
            Debug.LogError("Component not found: CameraController");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseInfo();
    }

    private void UpdateMouseInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            mouseTarget = ray.GetPoint(distance);
            distanceFromCharacterToMouse = Vector3.Distance(transform.position, mouseTarget);
        }
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
