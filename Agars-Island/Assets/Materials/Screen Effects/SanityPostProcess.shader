Shader "UrbanLizard/SanityPostProcess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VRadius("Vignette Radius", Range(0.0, 1.0)) = 0.8
        _VSoft("Vignette softness", Range(0.0, 1.0)) = 0.5
        _VColor("Vignette Color", Color) = (1,1,1,1)
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
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            // Properties
            
            float _VRadius;
            float _VSoft;
            float4 _VColor;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // add vignette
                float distFromCenter = distance(i.uv.xy, float2(0.5,0.5));
                
                float vignette = smoothstep(_VRadius, _VRadius - _VSoft, distFromCenter);
                col = saturate(col * vignette);
                
                col = col + (_VColor * (1 - vignette));
                
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
