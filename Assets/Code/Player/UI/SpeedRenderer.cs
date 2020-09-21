using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRenderer : MonoBehaviour
{
    public int numOfSteps = 17;
    public float inputAcceleration = 0.5f;
    private float[] coverageMappingEven;
    private float[] coverageMappingOdd;

    public bool IsOdd = false;
    public Image currentImage;

    private void Start()
    {
        currentImage = GetComponent<Image>();
        coverageMappingEven = new float[] 
        {
            0,0,7,7,16,16,28,28,40,40,52,52,65,65,80,80,93,93
        };
        coverageMappingOdd = new float[]
        {
            0,3,3,12,12,22,22,34,34,46,46,58,58,74,74,86,86,99
        };

    }

    void Update()
    {
        inputAcceleration = PlayerController.Singleton.acceleration;

        int currentSteps = Mathf.FloorToInt(inputAcceleration / (1f / numOfSteps));

        float coverage = (!IsOdd) ? coverageMappingEven[currentSteps] / 99f : coverageMappingOdd[currentSteps]/99f;

        currentImage.fillAmount = coverage;
    }
}
