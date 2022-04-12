using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightningController : MonoBehaviour
{
    public GameObject DisabledLightning;
    public GameObject EnabledLightning;

    private bool _isToggled { get; set; } = false;

    public void Start()
    {
        PlayerInputController.input_ToggleNightVision += ToggleNightVision;
    }

    public void ToggleNightVision()
    {
        _isToggled = !_isToggled;
        DisabledLightning.SetActive(!_isToggled);
        EnabledLightning.SetActive(_isToggled);
    }
}
