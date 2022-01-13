using UnityEngine;

public class HeightMapGenerator : MonoBehaviour
{
    public Color[] GenerateHeightMap(int width, int height, float scale, float octaves, float lacunarity, float persistence, float offsetX, float offsetZ, bool useFalloffMap, float falloffStrength)
    {
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        Material material = new Material(Shader.Find("Unlit/UnlitNoiseShader"));

        material.SetFloat("width", width);
        material.SetFloat("height", height);
        material.SetFloat("_Scale", scale);
        material.SetFloat("_Octaves", octaves);
        material.SetFloat("_Lacunarity", lacunarity);
        material.SetFloat("_Persistence", persistence);
        material.SetFloat("_OffsetX", offsetX);
        material.SetFloat("_OffsetZ", offsetZ);
        material.SetFloat("_UseFalloffMap", useFalloffMap == true ? 1 : 0);
        material.SetFloat("_FalloffStrength", falloffStrength);

        Color[] heightMap = new Color[width * height];

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        Graphics.Blit(material.mainTexture, renderTexture, material);

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        for (int i = 0; i < width * height; i++)
        {
            heightMap[i] = texture.GetPixel(i % width, i / width);
        }

        return heightMap;
    }
}
