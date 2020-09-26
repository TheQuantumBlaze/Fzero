using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class GroundRendering : MonoBehaviour
{
    public Texture2D trackImage;
    public MeshFilter mesh;
    public MeshRenderer render;
    public MeshCollider collider;
    public Map map;

    public int scale = 1;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>();
        render = GetComponent<MeshRenderer>();
        collider = GetComponent<MeshCollider>();
        map = new Map(trackImage, scale);

        CreateTrack();
    }

    private void CreateTrack()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        int width, height, scale, trisCounter = 0;

        width = map.width;
        height = map.height;
        scale = map.scale;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                int cx, cy;

                cx = x * scale;
                cy = y * scale;

                verts.AddRange(new Vector3[]{
                    new Vector3(cx+scale,0,cy + scale),
                    new Vector3(cx+scale,0,cy),
                    new Vector3(cx,0,cy),
                    new Vector3(cx,0,cy + scale)
                });

                tris.AddRange(new int[]
                {
                    trisCounter + 0,
                    trisCounter + 1,
                    trisCounter + 2,

                    trisCounter + 0,
                    trisCounter + 2,
                    trisCounter + 3
                });

                trisCounter += 4;

                int mapID = map.blockMapping[x,y];

                if (mapID == MAPINGS.TRACK)
                {
                    uvs.AddRange(new Vector2[]
                    {
                        new Vector2(0.5f,1f),
                        new Vector2(1f,1f),
                        new Vector2(1f,0f),
                        new Vector2(0.5f,0f)
                    });
                }
                else if(mapID == MAPINGS.NONTRACK)
                {
                    uvs.AddRange(new Vector2[]
                    {
                        new Vector2(0f,1f),
                        new Vector2(0.5f,1f),
                        new Vector2(0.5f,0f),
                        new Vector2(0f,0f)
                    });
                }
            }
        }


        if (mesh.mesh == null)
        {
            mesh.mesh = new Mesh();
        }

        mesh.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        print(verts.Count);
        print(uvs.Count);

        mesh.mesh.vertices = verts.ToArray();
        mesh.mesh.triangles = tris.ToArray();
        mesh.mesh.uv = uvs.ToArray();

        mesh.mesh.Optimize();
        mesh.mesh.OptimizeIndexBuffers();
        mesh.mesh.OptimizeReorderVertexBuffer();

        mesh.mesh.RecalculateBounds();
        mesh.mesh.RecalculateNormals();
        mesh.mesh.RecalculateTangents();

        collider.sharedMesh = mesh.mesh;

        GameManager.singleton.spawnPlayer(map);
    }
}
