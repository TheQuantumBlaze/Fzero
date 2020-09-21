using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GearDisplay : MonoBehaviour
{
    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.fillOrigin = (int)((PlayerController.Singleton.IsHighGear) ? Image.OriginVertical.Bottom : Image.OriginVertical.Top);
    }
}
