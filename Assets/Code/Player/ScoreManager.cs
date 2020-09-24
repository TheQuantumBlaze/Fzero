using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    public float Score = 0;
    public float BaseScore = 1;
    public float MultiplierForSpeed = 10;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }

    private void Update()
    {
        if(PlayerController.Singleton.acceleration > 0)
        {
            Score += BaseScore * (PlayerController.Singleton.acceleration * MultiplierForSpeed) * Time.deltaTime;
        }
    }
}
