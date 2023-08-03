using UnityEngine;
using System.Collections.Generic;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunkFab;

    Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();

    public int viewDistance;

    private void Update()
    {
        int scale = Island.main.chunkSize - 1;
        Vector2Int playerPos = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z)) / scale;

        for (int x = playerPos.x - viewDistance; x < playerPos.x + viewDistance; x++)
        {
            for (int z = playerPos.y - viewDistance; z < playerPos.y + viewDistance; z++)
            {
                Vector2Int pos = new Vector2Int(x, z);

                if (!chunks.ContainsKey(pos))
                {
                    GameObject chunk = Instantiate(chunkFab, new Vector3(pos.x * scale, 20, pos.y * scale), Quaternion.identity, Island.main.transform);
                    chunks.Add(pos, chunk);
                }
            }
        }
    }
}
