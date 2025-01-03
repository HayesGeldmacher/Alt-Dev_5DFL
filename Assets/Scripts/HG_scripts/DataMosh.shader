Shader "Unlit/DataMosh"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
      

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        	GrabPass
        {
            "_PR"
        }

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
            sampler2D _CameraMotionVectorsTexture;
            sampler2D _PR;
            int _Button;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
              float4 mot = tex2D(_CameraMotionVectorsTexture,i.uv);

                #if UNITY_UV_STARTS_AT_TOP
                float2 mvuv = float2(i.uv.x-mot.r,1-i.uv.y+mot.g);
                #else
                float2 mvuv = float2(i.uv.x-mot.r,i.uv.y-mot.g);
                #endif
                //inverse uv + appropriate displacement for motion vectors texture
                //also takes into account inverse UV values depending on graphics platform

                fixed4 col = lerp(tex2D(_MainTex,i.uv),tex2D(_PR, mvuv),_Button);
                return col;
            }
            ENDCG
        }
    }
}
