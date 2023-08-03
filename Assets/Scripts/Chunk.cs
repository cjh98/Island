using UnityEngine;

public class Chunk : MonoBehaviour
{
    MeshGenerator mg;

    private void Start()
    {
        mg = gameObject.AddComponent<MeshGenerator>();

        mg.GenerateChunkMesh(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z)), Island.main);

        // GENERATE TEXTURE
        Island.main.islandMaterial.SetFloat("minHeight", mg.minHeight);
        Island.main.islandMaterial.SetFloat("maxHeight", mg.maxHeight);

        mg.UpdateMesh();
    }
}
