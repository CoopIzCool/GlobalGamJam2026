Shader "Custom/NewSurfaceShader"
{
    // Google's AI created this hlsl for me.
    // I feel embarrassed.
    // This is a warning that this code is NOT mine.
    // I unfortunately had to throw in the towel on this.
    // Sadge. :<
    // - Dave
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Standard transparency blending
        Cull off // I did add this line myself though, now I feel like a jenius
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture color
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                // Invert the RGB channels, keeping the original alpha
                col.rgb = 1.0 - col.rgb;
                
                return col;
            }
            ENDCG
        }
    }
}