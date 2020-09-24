using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public Vector3 spawnPoint = new Vector3();

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
    }

    public void spawnPlayer()
    {
        PlayerController.Singleton.gameObject.transform.position = spawnPoint;
    }
}
