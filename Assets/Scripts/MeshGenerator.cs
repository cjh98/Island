using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    Vector2[] uvs;
    int[] tris;

    [System.NonSerialized]
    public float minHeight = float.MaxValue;
    [System.NonSerialized]
    public float maxHeight = float.MinValue;

    public void GenerateMesh(Island island, AnimationCurve heightsCurve)
    {
        vertices = new Vector3[island.width * island.width];
        tris = new int[6 * (island.width - 1) * (island.width - 1)];
        uvs = new Vector2[vertices.Length];
        int t;

        for (int i = 0; i < vertices.Length; i++)
        {
            int x = i % island.width;
            int y = i / island.width;

            int index = y * island.width + x;

            Vector3 pos = new Vector3(x, -island.heightMap[index].grayscale * island.heightScale, y);
            //pos -= Vector3.up * ;

            if (pos.y < minHeight)
                minHeight = pos.y;

            if (pos.y > maxHeight)
                maxHeight = pos.y;

            vertices[index] = pos;

            if (x != island.width - 1 && y != island.width - 1)
            {
                t = (y * (island.width - 1) + x) * 3 * 2;

                tris[t + 0] = index + island.width;
                tris[t + 1] = index + island.width + 1;
                tris[t + 2] = index;

                tris[t + 3] = index + island.width + 1;
                tris[t + 4] = index + 1;
                tris[t + 5] = index;

                t += 6;
            }

            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
    }

    public void UpdateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.Clear();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = tris;

        mesh.RecalculateNormals();
    }
}
