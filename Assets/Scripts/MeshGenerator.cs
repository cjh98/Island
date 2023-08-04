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

    public void GenerateChunkMesh(Vector2Int position, Island island, int dimensions)
    {
        vertices = new Vector3[(island.chunkSize) * (island.chunkSize)];
        tris = new int[6 * (island.chunkSize - 1) * (island.chunkSize - 1)];
        uvs = new Vector2[vertices.Length];
        int t;

        for (int i = 0; i < vertices.Length; i++)
        {
            int x = i % (island.chunkSize);
            int y = i / (island.chunkSize);

            int index = y * (island.chunkSize) + x;

            float x_2 = x * ((float)dimensions / (island.chunkSize - 1));
            float y_2 = y * ((float)dimensions / (island.chunkSize - 1));

            // makes sure not all sampling from the same place (0 - len)
            int heightMapX = Mathf.FloorToInt(x_2 + position.x);
            int heightMapY = Mathf.FloorToInt(y_2 + position.y);
            
            int heightIndex = heightMapY * island.width + heightMapX;

            Vector3 pos = new Vector3(
                x_2,
                -island.heightMap[heightIndex].grayscale * island.heightScale, 
                y_2);

            //if (pos.y < minHeight)
            //    minHeight = pos.y;

            //if (pos.y > maxHeight)
            //    maxHeight = pos.y;

            vertices[index] = pos;

            if (x != island.chunkSize - 1 && y != island.chunkSize - 1)
            {
                t = (y * (island.chunkSize - 1) + x) * 6;

                tris[t + 0] = index + island.chunkSize;             // top left
                tris[t + 1] = index + island.chunkSize + 1;         // top right
                tris[t + 2] = index;                                // bottom left

                tris[t + 3] = index + island.chunkSize + 1;         // top right
                tris[t + 4] = index + 1;                            // bottom right
                tris[t + 5] = index;                                // bottom left

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
