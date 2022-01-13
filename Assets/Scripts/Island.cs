using UnityEngine;

public class Island : MonoBehaviour
{
    public static Island main;

    public HeightMapGenerator heightMapGenerator;

    public bool autoUpdate = false;

    public Material islandMaterial;

    public AnimationCurve heightsCurve;

    public bool useFalloffMap;
    [Range(0.001f, 100f)]
    public float falloffStrength;
    public int width;
    public int height;
    [Range(0.00001f, 1000f)]
    public float scale;
    [Range(1, 32)]
    public float octaves;
    public float lacunarity;
    public float persistence;
    public float heightScale;
    public float offsetX;
    public float offsetZ;

    public RenderTexture renderTexture;
    public TextureData textureData;

    [System.NonSerialized]
    public Color[] heightMap;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        
        GenerateIsland();
    }

    public void GenerateIsland()
    {
        heightMap = heightMapGenerator.GenerateHeightMap(width, height, scale, octaves, lacunarity, persistence, offsetX, offsetZ, useFalloffMap, falloffStrength);

        MeshGenerator mg = GetComponent<MeshGenerator>();
        mg.GenerateMesh(this, heightsCurve);

        islandMaterial.SetFloat("minHeight", mg.minHeight);
        islandMaterial.SetFloat("maxHeight", mg.maxHeight);

        print(mg.minHeight + " " + mg.maxHeight);

        transform.position = new Vector3(0, -mg.minHeight, 0);

        mg.UpdateMesh();

        textureData.Apply();
    }
}
