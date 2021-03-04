Shader "UrbanLizard/SanityPostProcess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VPower ("Vignette Power", Range(0.0, 2.0)) = 0.8
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
            
            float _VPower;
            float4 _VColor;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // add vignette
                float distFromCenter = distance(i.uv.xy, float2(0.5,0.5));
                
                float vignette = (distFromCenter * _VPower);
                
                //return float4(vignette, vignette, vignette, 1.0f);
                
                
                
                float4 vignetteColor = float4(vignette, vignette, vignette, 1.0f) * _VColor;
                
                //return vignetteColor;
                
                float oppositeVignette = 1 - vignette;
                
                float4 oppVignette = float4(oppositeVignette, oppositeVignette, oppositeVignette, 1.0f);
                //return oppVignette;
                
                vignette = (vignetteColor + oppVignette);
                
                return float4(vignette, vignette, vignette, 1.0f);
                
                
                
                col = saturate(col * vignette);
                
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
