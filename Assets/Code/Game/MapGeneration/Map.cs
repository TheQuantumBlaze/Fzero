using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private Texture2D tex;
    public int[,] blockMapping;
    public Vector3 spawnPoint;
    public List<SpawnableEntites> spawnables;
    public int scale = 1;
    public int width, height;

    public Map(Texture2D tex, int scale)
    {
        this.tex = tex;
        this.scale = scale;
        width = tex.width;
        height = tex.height;
        blockMapping = new int[width, height];
        spawnables = new List<SpawnableEntites>();

        for (int x = 0; x < blockMapping.GetLength(0); x++)
        {
            for (int y = 0; y < blockMapping.GetLength(1); y++)
            {
                Color imageColor = tex.GetPixel(x, y);

                if (imageColor == Color.black)
                {
                    blockMapping[x, y] = MAPINGS.TRACK;
                }
                else if (imageColor == Color.red)
                {
                    blockMapping[x, y] = MAPINGS.TRACK;
                    spawnPoint = new Vector3(x * scale, 1.5f, y * scale);
                }
                else if(imageColor == Color.blue)
                {
                    blockMapping[x, y] = MAPINGS.TRACK;
                    spawnables.Add(new SpawnableEntites(ENTITYID.BARREL, new Vector3(x * scale, 0, y * scale)));
                }
                else if(imageColor == Color.green)
                {
                    blockMapping[x, y] = MAPINGS.NONTRACK;
                    spawnables.Add(new SpawnableEntites(ENTITYID.WALL, new Vector3(x * scale, 0, y * scale)));
                }
                else if(imageColor == Color.white)
                {
                    blockMapping[x, y] = MAPINGS.NONTRACK;
                }
            }
        }
    }
}

public static class MAPINGS
{
    public static int TRACK = 0;
    public static int NONTRACK = 1;
}

public static class ENTITYID
{
    public static int BARREL = 0;
    public static int WALL = 1;
}

public class SpawnableEntites
{
    public int ID;
    public Vector3 position;

    public SpawnableEntites(int id, Vector3 pos)
    {
        ID = id;
        position = pos;
    }
}

