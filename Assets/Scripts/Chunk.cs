using UnityEngine;

public class Chunk : MonoBehaviour
{
    MeshGenerator mg;

    public void GenerateMesh(Vector2Int position, int dimensions)
    {
        mg = gameObject.AddComponent<MeshGenerator>();
        mg.GenerateChunkMesh(position, Island.main, dimensions);

        // GENERATE TEXTURE
        Island.main.islandMaterial.SetFloat("minHeight", mg.minHeight);
        Island.main.islandMaterial.SetFloat("maxHeight", mg.maxHeight);

        mg.UpdateMesh();
    }
}
