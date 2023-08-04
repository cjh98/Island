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

        foreach (QuadTreeNode node in QuadTreeNode.AllNodes)
        {
            if (node.Children.Count == 0)
            {
                GameObject chunkObject = Instantiate(chunkFab, new Vector3(node.Bounds.Min.x, Island.main.heightScale, node.Bounds.Min.y), Quaternion.identity, Island.main.transform);
                Chunk chunk = chunkObject.GetComponent<Chunk>();

                chunk.GenerateMesh(
                    new Vector2Int(Mathf.FloorToInt(chunkObject.transform.position.x), Mathf.FloorToInt(chunkObject.transform.position.z)),
                    node.Bounds.GetSize());
            }
        }
        
    }

    private void Update()
    {

    }
}
