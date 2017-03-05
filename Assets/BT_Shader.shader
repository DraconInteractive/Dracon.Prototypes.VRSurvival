Shader "Custom/BT_Shader" {
	SubShader {
		Tags {"Queue" = "Geometry+10"}

		ColorMask 0
		ZWrite On

		Pass {
			Cull Off
		}
	}
}
