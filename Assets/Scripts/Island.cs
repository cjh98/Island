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
    public int chunkSize;
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

    public Chunk chunkFab;

    void Awake()
    {
        main = this;
        GenerateIsland();
    }

    public void GenerateIsland()
    {
        heightMap = heightMapGenerator.GenerateHeightMap(width, height, scale, octaves, lacunarity, persistence, offsetX, offsetZ, useFalloffMap, falloffStrength);
    }
}
