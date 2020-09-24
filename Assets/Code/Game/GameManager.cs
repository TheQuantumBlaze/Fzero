using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public Vector3 spawnPoint = new Vector3();

    public GameObject barrelPrefab;

    public List<Vector3> barrelSpawn = new List<Vector3>();

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
    }

    public void spawnPlayer()
    {
        SpawnBarrels();
        PlayerController.Singleton.gameObject.transform.position = spawnPoint;
    }

    public void SpawnBarrels()
    {
        foreach(Vector3 vec in barrelSpawn)
        {
            GameObject go = Instantiate(barrelPrefab, vec, Quaternion.identity);
        }
    }
}
