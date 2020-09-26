using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public GameObject barrelPrefab;
    public Map currentMap;

    
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
    }

    public void spawnPlayer(Map map)
    {
        this.currentMap = map;
        SpawnBarrels();
        PlayerController.Singleton.gameObject.transform.position = map.spawnPoint;
    }

    public void SpawnBarrels()
    {
        foreach (var s in currentMap.spawnables)
        {
            if (s.ID == ENTITYID.BARREL)
            {
                GameObject go = Instantiate(barrelPrefab, s.position, Quaternion.identity);
            }
        }
    }
}
