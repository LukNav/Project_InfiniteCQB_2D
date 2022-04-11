using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISourceImageSwapper : MonoBehaviour
{
    public Image Image;
    public Sprite Enabled;
    public Sprite Disabled;
    private bool _isEnabled { get; set; } = false;

    public void SwapImages()
    {
        _isEnabled = !_isEnabled;
        this.Image.sprite = _isEnabled ? Enabled : Disabled;
    }
}
