using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextRender : MonoBehaviour
{
    public Text speedText;

    private void Start()
    {
        speedText = GetComponent<Text>();
    }

    private void Update()
    {
        speedText.text = (Mathf.RoundToInt(PlayerController.Singleton.movementSpeed * PlayerController.Singleton.acceleration)).ToString() + " KM/H";
    }
}
