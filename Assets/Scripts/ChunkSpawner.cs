using UnityEngine;
using System.Collections.Generic;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunkFab;

    Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();

    public int viewDistance;

    private void Start()
    {
        QuadTreeNode qt = new QuadTreeNode(new Box(new Vector2Int(0, 0), new Vector2Int(2000, 2000)));

        qt.Insert(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z)));

        print(QuadTreeNode.AllNodes.Count);

        foreach (QuadTreeNode node in QuadTreeNode.AllNodes)
        {
            Instantiate(chunkFab, new Vector3(node.Bounds.Min.x, Island.main.heightScale, node.Bounds.Min.y), Quaternion.identity, Island.main.transform);
        }
        
    }

    private void Update()
    {
    //    int scale = Island.main.chunkSize - 1;
    //    Vector2Int playerPos = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z)) / scale;

    //    for (int x = playerPos.x - viewDistance; x < playerPos.x + viewDistance; x++)
    //    {
    //        for (int z = playerPos.y - viewDistance; z < playerPos.y + viewDistance; z++)
    //        {
    //            Vector2Int pos = new Vector2Int(x, z);

    //            if (!chunks.ContainsKey(pos))
    //            {
    //                GameObject chunk = Instantiate(chunkFab, new Vector3(pos.x * scale, Island.main.heightScale, pos.y * scale), Quaternion.identity, Island.main.transform);
    //                chunks.Add(pos, chunk);
    //            }
    //        }
    //    }
    }
}
