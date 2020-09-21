using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Billboard : MonoBehaviour
{

    private MeshRenderer render;
    private MeshFilter filter;

    public Material Image;
    public float width = 1, height = 1;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    void GenerateMesh()
    { 
        List<Vector3> verts = new List<Vector3>();
        List<int> tries = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        verts.AddRange(new Vector3[]
        {
            new Vector3(0+width/2f,0+height,0),
            new Vector3(0+width/2f,0,0),
            new Vector3(0-width/2f,0,0),
            new Vector3(0-width/2f,0+height,0)
        });

        tries.AddRange(new int[]
        {
            0,
            1,
            2,

            0,
            2,
            3
        });

        uvs.AddRange(new Vector2[]
        {
            new Vector2(1f,1f),
            new Vector2(1f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,1f),
        });

        if (filter.mesh == null)
        {
            filter.mesh = new Mesh();
        }

        filter.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        filter.mesh.vertices = verts.ToArray();
        filter.mesh.triangles = tries.ToArray();
        filter.mesh.uv = uvs.ToArray();

        filter.mesh.Optimize();
        filter.mesh.OptimizeIndexBuffers();
        filter.mesh.OptimizeReorderVertexBuffer();

        filter.mesh.RecalculateBounds();
        filter.mesh.RecalculateNormals();
        filter.mesh.RecalculateTangents();

        render.material = Image;
    }

    public void Update()
    {
        Vector2 rot = new Vector2(Camera.main.transform.position.x - this.transform.position.x, Camera.main.transform.position.z - this.transform.position.z);
        var rotation = Mathf.Atan2(rot.y, rot.x);
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, -rotation * Mathf.Rad2Deg - 90, this.transform.rotation.eulerAngles.z);
    }
}
