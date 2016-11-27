using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteSheetManager : MonoBehaviour {
	// スプライトシートに含まれるスプライトをキャッシュするディクショナリー
	private static Dictionary<string, Dictionary<string, Sprite>> spriteSheets = 
		new Dictionary<string, Dictionary<string, Sprite>>();

	// スプライトシートに含まれるスプライトを読み込んでキャッシュするメソッド
	public static void Load(string path)
	{
		if (!spriteSheets.ContainsKey (path)) {
			spriteSheets.Add (path, new Dictionary<string, Sprite> ());
		}

		// スプライトを読み込む
		Sprite[] sprites = Resources.LoadAll<Sprite>(path);
		foreach (var sprite in sprites) {
			if (!spriteSheets [path].ContainsKey (sprite.name)) {
				spriteSheets [path].Add (sprite.name, sprite);
			}
		}
	}

	// スプライト名からスプライトシートに含まれるスプライトを返すメソッド
	public static Sprite GetSpriteByName(string path, string name){
		if (spriteSheets.ContainsKey (path) && spriteSheets [path].ContainsKey (name)) {
			return spriteSheets [path] [name];
		}
		return null;
	}
}
