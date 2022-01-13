// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/UnlitNoiseShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        [Toggle] _UseFalloffMap("Use Falloff Map", float) = 0
        _FalloffStrength("Falloff Strength", float) = 1
        _Scale("Scale", Range(0.00001, 1000)) = 1
        _Octaves("Octaves", Range(1, 32)) = 1
        _Lacunarity("Lacunarity", float) = 2
        _Persistence("Persistence", float) = 0.5
        _OffsetX("OffsetX", float) = 0
        _OffsetZ("OffsetZ", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
            };

            float width;
            float height;

            float _UseFalloffMap;
            float _FalloffStrength;
            float4 _MainTex_ST;
            float _Scale;
            int _Octaves;
            float _Lacunarity;
            float _Persistence;
            float _OffsetX;
            float _OffsetZ;

            float minHeight;
            float maxHeight;

            float hash(float n)
            {
                return frac(sin(n) * 43758.5453);
            }

            float noise(float3 x)
            {
                float3 p = floor(x);
                float3 f = frac(x);

                f = f * f * (3.0 - 2.0 * f);
                float n = p.x + p.y * 57.0 + 113.0 * p.z;

                return lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),
                    lerp(hash(n + 57.0), hash(n + 58.0), f.x), f.y),
                    lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),
                        lerp(hash(n + 170.0), hash(n + 171.0), f.x), f.y), f.z);
            }

            v2f vert (appdata v)
            {
                v2f o;
                // transform position to clip space
                // (multiply with model*view*projection matrix)
                o.vertex = UnityObjectToClipPos(v.vertex);
                // just pass the texture coordinate
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float weight = 1;
                float n = 0;
                for (int octave = 0; octave < _Octaves; octave++)
                {
                    n += noise(float3(i.vertex.x + _OffsetX, i.vertex.y + _OffsetZ, i.vertex.z) / _Scale) * weight;
                    _Scale /= _Lacunarity;
                    weight *= _Persistence;
                }

                if (_UseFalloffMap == 1)
                {
                    float x = (i.vertex.x / width * 2 - 1) * _FalloffStrength;
                    float y = (i.vertex.y / height * 2 - 1) * _FalloffStrength;

                    n += max(abs(x), abs(y));
                }

                return n;
            }
            ENDCG
        }
    }
}
