using UnityEngine;
using System.Collections;

public static class TextureGenerator {

	public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height) {
		Texture2D texture = new Texture2D (width, height);
		Debug.Log (colorMap.Length);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels (colorMap);
		texture.Apply ();
		Debug.Log (texture.height);
		return texture;
	}
}