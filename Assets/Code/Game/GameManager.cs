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
        GameObject barrelsParent = new GameObject("Barrel-Parrent");
        GameObject wallParent = new GameObject("WallParents");

        foreach (var s in currentMap.spawnables)
        {
            if (s.ID == ENTITYID.BARREL)
            {
                GameObject go = Instantiate(barrelPrefab, s.position, Quaternion.identity);
                go.transform.parent = barrelsParent.transform;
            }
            if(s.ID == ENTITYID.WALL)
            {
                GameObject go = new GameObject("Wall");
                go.transform.position = s.position;
                var b = go.AddComponent<BoxCollider>();
                b.isTrigger = true;
                b.size = new Vector3(GameManager.singleton.currentMap.scale, 10, GameManager.singleton.currentMap.scale);
                b.center = new Vector3(GameManager.singleton.currentMap.scale / 2, 10 / 2, GameManager.singleton.currentMap.scale / 2);
                go.tag = "CrashableWall";
                go.transform.parent = wallParent.transform;
            }
        }
    }
}
